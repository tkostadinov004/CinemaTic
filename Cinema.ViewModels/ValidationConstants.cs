using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    public static class ValidationConstants
    {
        public const string YoutubeTrailerRegex = "(.*?)(^|\\/|v=)([a-zA-Z0-9_-]{11})(.*)?";
        public const string HexColorRegex = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

        public const int ActorFullNameMinLength = 5;
        public const int ActorFullNameMaxLength = 300;
        public const string RatingMinValue = "1.0";
        public const string RatingMaxValue = "10.0";

        public const int GenreNameMinLength = 2;
        public const int GenreNameMaxLength = 100;

        public const int MovieTitleMinLength = 2;
        public const int MovieTitleMaxLength = 300;

        public const int MovieDescriptionMinLength = 50;
        public const int MovieDescriptionMaxLength = 1000;

        public const int DirectorNameMinLength = 5;
        public const int DirectorNameMaxLength = 300;

        public const int CinemaNameMinLength = 5;
        public const int CinemaNameMaxLength = 100;

        public const int CinemaDescriptionMinLength = 50;
        public const int CinemaDescriptionMaxLength = 1000;

        public const int MinimumRowsCount = 2;
        public const int MaximumRowsCount = 100;

        public const int MinimumColsCount = 2;
        public const int MaximumColsCount = 100;
    }
}
