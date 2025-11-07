using Microsoft.EntityFrameworkCore;
using ResultsService.Data;
using ResultsService.Models;

namespace ResultsService.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly ResultsServiceContext _context;

        public ResultRepository(ResultsServiceContext context)
        {
            _context = context;
        }

        //public User? GetUser(long userId) => _context.Users.Find(userId);

        //public Quiz? GetQuiz(long quizId) => _context.Quizzes.Find(quizId);

        //public Question? GetQuestion(long questionId) => _context.Questions.Find(questionId);

        public void AddResult(UserQuizResult result)
        {
            _context.UserQuizResults.Add(result);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<UserQuizResult> GetResultsByQuiz(long quizId)
        {
            return _context.UserQuizResults
                .Include(r => r.UserAnswers)
                .Where(r => r.QuizId == quizId)
                .ToList();
        }

        public IEnumerable<UserQuizResult> GetResultsByUser(long userId)
        {
            return _context.UserQuizResults
                .Include(r => r.UserAnswers)
                .Where(r => r.UserId == userId)
                .ToList();
        }

        public IEnumerable<UserQuizResult> GetAllResults()
        {
            return _context.UserQuizResults
                .Include(r => r.UserAnswers)
                .ToList();
        }

        public UserQuizResult? GetResultById(long resultId)
        {
            return _context.UserQuizResults
                .Include(r => r.UserAnswers)
                .FirstOrDefault(r => r.Id == resultId);
        }
    }
}
