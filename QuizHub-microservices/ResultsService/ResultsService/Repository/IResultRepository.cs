using ResultsService.Models;

namespace ResultsService.Repository
{
    public interface IResultRepository
    {
        IEnumerable<UserQuizResult> GetAllResults();
        IEnumerable<UserQuizResult> GetResultsByQuiz(long quizId);
        IEnumerable<UserQuizResult> GetResultsByUser(long userId);
        UserQuizResult? GetResultById(long resultId);
        void AddResult(UserQuizResult result);
        void SaveChanges();
    }
}
