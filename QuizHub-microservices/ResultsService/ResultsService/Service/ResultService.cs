using ResultsService.DTOs;
using ResultsService.Models;
using ResultsService.Repository;

namespace ResultsService.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repo;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        private readonly string _userServiceBaseUrl;
        private readonly string _quizServiceBaseUrl;

        public ResultService(IResultRepository repo, HttpClient httpClient, IConfiguration config)
        {
            _repo = repo;
            _httpClient = httpClient;
            _config = config;

            _userServiceBaseUrl = _config["Services:UserService"] ?? "http://userservice/api/users";
            _quizServiceBaseUrl = _config["Services:QuizService"] ?? "http://quizservice/api/quizzes";
        }

        // ===================== API POMOĆNE METODE =====================

        private async Task<ExternalUserDto?> GetUserAsync(long userId)
        {
            var response = await _httpClient.GetAsync($"{_userServiceBaseUrl}/{userId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ExternalUserDto>();
        }

        private async Task<ExternalQuizDto?> GetQuizAsync(long quizId)
        {
            var response = await _httpClient.GetAsync($"{_quizServiceBaseUrl}/{quizId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ExternalQuizDto>();
        }

        private async Task<ExternalQuestionDto?> GetQuestionAsync(long questionId)
        {
            var response = await _httpClient.GetAsync($"{_quizServiceBaseUrl}/questions/{questionId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<ExternalQuestionDto>();
        }

        // ===================== GLAVNE METODE =====================

        public QuizResultDto? SaveResult(SaveResultDto dto)
        {
            // ⚠️ HttpClient metode su async, ali kontroler koristi sinhrono
            // pa se ovde koristi .Result samo da se očuva isti potpis
            var user = GetUserAsync(dto.UserId).Result;
            var quiz = GetQuizAsync(dto.QuizId).Result;

            if (user == null || quiz == null) return null;

            var userAnswers = new List<UserAnswer>();

            if (dto.UserAnswers != null)
            {
                foreach (var a in dto.UserAnswers)
                {
                    var question = GetQuestionAsync(a.QuestionId).Result;
                    if (question != null)
                    {
                        userAnswers.Add(new UserAnswer
                        {
                            QuestionId = question.Id,
                            SelectedAnswer = a.SelectedAnswer
                        });
                    }
                }
            }

            var result = new UserQuizResult
            {
                UserId = user.Id,
                QuizId = quiz.Id,
                Score = dto.Score,
                TimeTaken = dto.TimeTaken,
                DatePlayed = DateTime.UtcNow,
                UserAnswers = userAnswers
            };

            _repo.AddResult(result);
            _repo.SaveChanges();

            return new QuizResultDto
            {
                Id = result.Id,
                Score = result.Score,
                TimeTaken = result.TimeTaken,
                DatePlayed = result.DatePlayed,
                Username = user.Username
            };
        }

        public IEnumerable<QuizResultDto> GetResultsByQuiz(long quizId)
        {
            var results = _repo.GetResultsByQuiz(quizId);
            return results
                .OrderByDescending(r => r.Score)
                .Select(r => new QuizResultDto
                {
                    Id = r.Id,
                    Score = r.Score,
                    TimeTaken = r.TimeTaken,
                    DatePlayed = r.DatePlayed
                })
                .ToList();
        }

        public IEnumerable<LeaderboardEntryDto> GetGlobalLeaderboard()
        {
            var results = _repo.GetAllResults();

            return results
                .GroupBy(r => r.UserId)
                .Select(g => new LeaderboardEntryDto
                {
                    UserId = g.Key,
                    TotalScore = g.Sum(x => x.Score),
                    QuizzesTaken = g.Count()
                })
                .OrderByDescending(x => x.TotalScore)
                .ToList();
        }

        //public IEnumerable<QuizResultDto> GetResultsByUser(long userId)
        //{
        //    var results = _repo.GetResultsByUser(userId);
        //    return results
        //        .Select(r => new QuizResultDto
        //        {
        //            Id = r.Id,
        //            Score = r.Score,
        //            TimeTaken = r.TimeTaken,
        //            DatePlayed = r.DatePlayed
        //        })
        //        .ToList();
        //}

        public IEnumerable<UserAnswersDto> GetUserAnswers(long resultId)
        {
            var result = _repo.GetResultById(resultId);
            if (result == null) return Enumerable.Empty<UserAnswersDto>();

            return result.UserAnswers.Select(a => new UserAnswersDto
            {
                //QuestionId = a.QuestionId,
                SelectedAnswer = a.SelectedAnswer
                //CorrectAnswer = a.CorrectAnswer
            });
        }

        public IEnumerable<TopResultDto> GetTopResults(long? quizId, string? period)
        {
            var results = _repo.GetAllResults();

            if (quizId.HasValue)
                results = results.Where(r => r.QuizId == quizId.Value);

            // Filtar po vremenskom periodu
            if (!string.IsNullOrEmpty(period))
            {
                var now = DateTime.UtcNow;
                results = period.ToLower() switch
                {
                    "day" => results.Where(r => r.DatePlayed >= now.AddDays(-1)),
                    "week" => results.Where(r => r.DatePlayed >= now.AddDays(-7)),
                    "month" => results.Where(r => r.DatePlayed >= now.AddMonths(-1)),
                    _ => results
                };
            }

            var topResults = results
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.TimeTaken)
                .Take(10)
                .Select(r => new TopResultDto
                {
                    QuizId = r.QuizId,
                    Username = "N/A", // ako želiš, možeš ovde opet GetUserAsync(r.UserId).Result
                    Score = r.Score,
                    TimeTaken = r.TimeTaken,
                    DatePlayed = r.DatePlayed
                })
                .ToList();

            return topResults;
        }

        public IEnumerable<AllQuizResultDto> GetResultsByUser(long userId)
        {
            var results = _repo.GetAllResults()
                .Where(r => r.UserId == userId)
                .ToList();

            var resultDtos = new List<AllQuizResultDto>();

            foreach (var r in results)
            {
                var user = GetUserAsync(r.UserId).Result;
                var quiz = GetQuizAsync(r.QuizId).Result;

                var dto = new AllQuizResultDto
                {
                    Id = r.Id,
                    Score = r.Score,
                    TimeTaken = r.TimeTaken,
                    DatePlayed = r.DatePlayed,
                    Username = user?.Username ?? "N/A",
                    QuizTitle = quiz?.Title ?? "Unknown Quiz",
                    TotalQuestions = quiz?.TotalQuestions ?? 0
                };

                resultDtos.Add(dto);
            }

            return resultDtos.OrderByDescending(r => r.DatePlayed).ToList();
        }
    }
}
