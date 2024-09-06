using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public User Student { get; set; } = null!;
        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;
        [Column(TypeName = "decimal(2,1)")]
        public decimal Value { get; set; }

        public string? Description { get; set; }

    }
}
