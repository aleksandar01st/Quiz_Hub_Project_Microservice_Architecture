using System.ComponentModel.DataAnnotations;

namespace ResultsService.Models
{
    public class UserQuizResult
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int TimeTaken { get; set; } // u sekundama

        [Required]
        public DateTime DatePlayed { get; set; }

        [Required]
        public long UserId { get; set; }
        //public User User { get; set; }

        [Required]
        public long QuizId { get; set; }
        //public Quiz Quiz { get; set; }

        //public ICollection<UserAnswer> UserAnswers { get; set; }

        // Samo referenca na UserAnswer unutar ovog servisa
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
