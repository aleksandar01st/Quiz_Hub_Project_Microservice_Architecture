using ResultsService.DTOs;

namespace ResultsService.Service
{
    public interface IResultService
    {
        QuizResultDto? SaveResult(SaveResultDto dto);
        IEnumerable<QuizResultDto> GetResultsByQuiz(long quizId);
        IEnumerable<LeaderboardEntryDto> GetGlobalLeaderboard();
        IEnumerable<AllQuizResultDto> GetResultsByUser(long userId);
        IEnumerable<UserAnswersDto> GetUserAnswers(long resultId);
        IEnumerable<TopResultDto> GetTopResults(long? quizId = null, string? period = null);
    }
}
