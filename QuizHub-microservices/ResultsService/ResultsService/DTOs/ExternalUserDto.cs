namespace ResultsService.DTOs
{
    public class ExternalUserDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }

    public class ExternalQuizDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; }
        public int TimeLimit { get; set; }
        public int TotalQuestions { get; set; }
    }

    public class ExternalQuestionDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<ExternalAnswerOptionDto> AnswerOptions { get; set; } = new();
    }

    public class ExternalAnswerOptionDto
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
