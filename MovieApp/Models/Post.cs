using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public String UserId { get; set; }
    }
}
