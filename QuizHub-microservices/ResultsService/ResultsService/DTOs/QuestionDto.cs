namespace ResultsService.DTOs
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<AnswerOptionDto> AnswerOptions { get; set; } = new List<AnswerOptionDto>();
    }

    public class AnswerOptionDto
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
