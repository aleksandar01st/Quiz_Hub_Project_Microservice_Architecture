using QuizService.DTOs;

namespace QuizService.Service
{
    public interface IQuestionService
    {
        IEnumerable<QuestionDto> GetAll();
        QuestionDto? GetById(long id);
        IEnumerable<QuestionDto> GetByQuizId(long quizId);
        QuestionDto Create(CreateQuestionDto dto);
        QuestionDto? Update(long id, UpdateQuestionDto dto);
        bool Delete(long id);
    }
}
