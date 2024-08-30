namespace E_Learning.DAL.Models
{
    public class Answer : BaseEntity
    {
        public string? Body { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
