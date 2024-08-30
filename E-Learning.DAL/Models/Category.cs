namespace E_Learning.DAL.Models
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public List<Course> Courses { get; set; } = null!;
    }
}
