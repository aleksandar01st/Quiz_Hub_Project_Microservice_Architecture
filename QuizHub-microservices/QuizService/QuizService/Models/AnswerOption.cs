using System.ComponentModel.DataAnnotations;

namespace QuizService.Models
{
    public class AnswerOption
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
