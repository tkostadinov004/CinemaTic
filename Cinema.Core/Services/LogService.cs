using Cinema.Data.Models;
using Cinema.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Data.Enums;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Diagnostics;

namespace Cinema.Core.Services
{
    public static class LogService
    {
        public static IHttpContextAccessor _httpContextAccessor;
        public static UserManager<ApplicationUser> _userManager;
        public static CinemaDbContext _context;
        public static async Task<ApplicationUser> GetUser()
        {
            var userEmail = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await _userManager.FindByEmailAsync(userEmail);
        }
        public static void AddAction(object data)
        {
            //_context.UserActions.Add(new UserAction
            //{
            //    Id = Guid.NewGuid(),
            //    Type = type,
            //    UserId = GetUser().Result.Id,
            //    Message = message
            //});
            _context.SaveChanges();
        }
    }
}
