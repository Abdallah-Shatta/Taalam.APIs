﻿namespace E_Learning.DAL.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
