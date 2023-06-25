using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
  
    public class FollowUser
    {
        [Key]
        public int FollowId { get; set; }
        public string FollowerUserId { get; set; } // Takip eden kullanıcının kimliği
        public string FollowingId { get; set; } // Takip edilen kullanıcının kimliği
       
    }
}
