using QuizService.Models;

namespace QuizService.Repository
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetAll();
        Quiz? GetById(long id);
        void Add(Quiz quiz);
        void Update(Quiz quiz);
        void Delete(Quiz quiz);
        IEnumerable<string> GetCategories();
        void SaveChanges();
    }
}
