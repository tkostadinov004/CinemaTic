using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTic.Data.Models
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public ApplicationUser(string id, int accessFailedCount, string concurrencyStamp, DateTime creationDate, string email, bool emailConfirmed, string firstName, string lastName, bool lockoutEnabled, DateTimeOffset? lockoutEnd, string normalizedEmail, string normalizedUsername, string password, string phoneNumber, bool phoneNumberConfirmed, string profilePictureUrl, string securityStamp, bool twoFactorEnabled, string userName)
        {
            Id = id;
            AccessFailedCount = accessFailedCount;
            ConcurrencyStamp = concurrencyStamp;
            CreationDate = creationDate;
            Email = email;
            EmailConfirmed = emailConfirmed;
            FirstName = firstName;
            LastName = lastName;
            LockoutEnabled = lockoutEnabled;
            LockoutEnd = lockoutEnd;
            NormalizedEmail = normalizedEmail;
            NormalizedUserName = normalizedUsername;
            PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(this, email.StartsWith("owner") ? "ownerPass123*" : (email.StartsWith("customer") ? "customerPass123*" : "adminPass123*"));
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            ProfilePictureUrl = profilePictureUrl;
            SecurityStamp = securityStamp;
            TwoFactorEnabled = twoFactorEnabled;
            UserName = userName;
        }

        [Display(Name = "First name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required, MaxLength(100), RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Cinema> CinemasOwned { get; set; } = new List<Cinema>();
        public virtual ICollection<Movie> MoviesAdded { get; set; } = new List<Movie>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<CustomerCinema> CinemasVisited { get; set; } = new List<CustomerCinema>();
        public virtual ICollection<ActionLog> UserActions { get; set; } = new List<ActionLog>();
    }
}
