using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public static class LogMessages
    {
        public const string PurchaseTicketMessage = "Bought a ticket ({0}) for movie \"{1}\" in cinema \"{2}\" for date {3}";
        public const string AddEntityMessage = "Added {0} - \"{1}\" {2}";
        public const string DeleteEntityMessage = "Deleted {0} - \"{1}\" {2}";
        public const string EditEntityMessage = "Edited {0} - \"{1}\" {2}";
        public const string ChangePasswordMessage = "Changed password";
        public const string AddCinemaToFavoritesMessage = "Added cinema \"{0}\" to favorites";
        public const string RemoveCinemaToFavoritesMessage = "Removed cinema \"{0}\" from favorites";
        public const string RateMovieMessage = "Rated movie \"{0}\" with a rating of {1}";
        public const string ChangeRatingMovieMessage = "Changed rating for movie \"{0}\" from {1} to {2}";
        public const string AddMovieToCinemaMessage = "Added movie \"{0}\" to cinema \"{1}\"";
        public const string UserLoginMessage = "User logged in";
        public const string UserRegisterMessage = "User was registered";
        public const string UserLogoutMessage = "User logged out";
    }
}
