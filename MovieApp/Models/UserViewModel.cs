using MovieApp.Areas.Identity.Data;

namespace MovieApp.Models
{
    public class UserViewModel
    {
        public List<User> NotFollowedUsers { get; set; }
        public List<User> FollowedUsers { get; set; }
    }
}
