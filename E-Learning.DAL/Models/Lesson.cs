namespace E_Learning.DAL.Models
{
    public class Lesson : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Duration { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}
