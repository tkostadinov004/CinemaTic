using CinemaTic.Data.Models;
using CinemaTic.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaTic.Data.Enums;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Diagnostics;
using CinemaTic.Core.Contracts;
using System.Security.Principal;

namespace CinemaTic.Core.Services
{
    public class LogService : ILogService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogService(UserManager<ApplicationUser> userManager, CinemaDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// <para>Adds a log message to the database.</para>
        /// </summary>
        public async Task LogActionAsync(UserActionType type, string message, params object[] attributes)
        {
            var user = await this.GetUser();
            if (user != null)
            {
                _context.ActionLogs.Add(new ActionLog
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    UserId = user.Id,
                    Date = DateTime.Now,
                    Message = $"{string.Format(message, attributes.Select(i => i.ToString()).ToArray()).Trim()}"
                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task<ApplicationUser> GetUser()
        {
            return await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name ?? "");
        }
    }
}
