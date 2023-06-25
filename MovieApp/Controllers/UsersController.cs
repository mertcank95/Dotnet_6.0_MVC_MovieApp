using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly MovieAppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(MovieAppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult UsersView()
        {
            string currentUserId = _userManager.GetUserId(User);
            List<string> followedUserIds = _context.FollowUsers
                .Where(f => f.FollowerUserId == currentUserId)
                .Select(f => f.FollowingId)
                .ToList();
            followedUserIds.Add(currentUserId);
            List<User> notFollowedUsers = _context.Users
                .Where(u => !followedUserIds.Contains(u.Id))
                .ToList();

            List<User> followedUsers = _context.Users
                .Where(u => followedUserIds.Contains(u.Id))
                .ToList();

            UserViewModel viewModel = new UserViewModel
            {
                NotFollowedUsers = notFollowedUsers,
                FollowedUsers = followedUsers
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FollowUser(string userId)
        {
            string currentUserId = _userManager.GetUserId(User);  // Mevcut kullanıcının kimliği
            FollowUser follow = new FollowUser
            {
                FollowerUserId = currentUserId,
                FollowingId = userId
            };
            _context.FollowUsers.Add(follow);
            await _context.SaveChangesAsync();
            return RedirectToAction("UsersView");
        }



        [HttpPost]
        public async Task<IActionResult> UnfollowUser(string userId)
        {
            string currentUserId = _userManager.GetUserId(User);
            FollowUser follow = _context.FollowUsers
                .FirstOrDefault(f => f.FollowerUserId == currentUserId && f.FollowingId == userId);

            if (follow != null)
            {
                _context.FollowUsers.Remove(follow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("UsersView");
        }



    }
}
