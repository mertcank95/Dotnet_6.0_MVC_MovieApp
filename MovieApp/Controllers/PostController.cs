using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class PostController : Controller
    {
        private readonly MovieAppDbContext _context;
        private readonly UserManager<User> _userManager;
        public PostController(MovieAppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult PostCreate()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Post post)
        {
            
                    post.UserId = _userManager.GetUserId(User);
                    post.CreatedAt = DateTime.Now;
                    _context.Posts.Add(post);
                    _context.SaveChanges();
                    TempData["ResultOk"] = "Record added";
                    return RedirectToAction("Index","Home");
           
        }


        public IActionResult UserPosts()
        {
            var userId= _userManager.GetUserId(User);
            var userPosts = _context.Posts.Where(p => p.UserId == userId).ToList();
            return View("MyPost",userPosts);
        }


        [HttpPost]
        public IActionResult DeletePost(int postId)
        {
            var post = _context.Posts.Find(postId);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

            TempData["ResultOk"] = "Post deleted";
            return  RedirectToAction("MyPost");
        }

        public IActionResult MyPost()
        {
            var userId = _userManager.GetUserId(User);
            var userPosts = _context.Posts.Where(p => p.UserId == userId).ToList();
            return View(userPosts);
        }

        public IActionResult UserPost()
        {
            string currentUserId = _userManager.GetUserId(User);
            List<string> followedUserIds = _context.FollowUsers
                .Where(f => f.FollowerUserId == currentUserId)
                .Select(f => f.FollowingId)
                .ToList();

            List<Post> followedUserPosts = _context.Posts
                .Where(p => followedUserIds.Contains(p.UserId))
                .ToList();

            return View(followedUserPosts);
        }


    }
}
