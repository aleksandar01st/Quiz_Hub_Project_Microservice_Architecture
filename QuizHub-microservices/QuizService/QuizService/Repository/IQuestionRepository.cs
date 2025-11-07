using QuizService.Models;

namespace QuizService.Repository
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAll();
        Question? GetById(long id);
        IEnumerable<Question> GetByQuizId(long quizId);
        Question Add(Question question);
        void Update(Question question);
        void Delete(Question question);
        void Save();
    }
}
