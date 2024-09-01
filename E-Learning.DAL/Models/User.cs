using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace E_Learning.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string? FName { get; set; }
       public string? LName { get; set; }
        //[DefaultValue("../../aa.jpeg")]
        public string? ProfilePicture { get; set; }
        public List<Course>? OwnedCourses { get; set; }
        public List<Cart>? CartItems { get; set; }
        public List<Enrollment>? UserEnrollments { get; set; }
    }
}
