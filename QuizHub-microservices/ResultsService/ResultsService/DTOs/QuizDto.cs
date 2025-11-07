namespace ResultsService.DTOs
{
    public class QuizDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int TimeLimit { get; set; }
        public int TotalQuestions { get; set; } 
    }
}
