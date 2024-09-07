using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int Value { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; } 
    }
}
