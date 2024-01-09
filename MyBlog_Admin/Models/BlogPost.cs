using System.ComponentModel;

namespace MyBlog_Admin.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public string? FeaturedImagePath { get; set; }
        public bool IsPublish { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
