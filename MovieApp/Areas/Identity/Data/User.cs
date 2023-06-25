using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using MovieApp.Models;

namespace MovieApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
   
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<User> FollowedUsers { get; set; } // Takip edilen kullanıcılar
    public List<User> FollowingUsers { get; set; } // Takip edilen tarafından takip edilen kullanıcılar
    public List<Post> Posts { get; set; } // Kullanıcının postları
}

