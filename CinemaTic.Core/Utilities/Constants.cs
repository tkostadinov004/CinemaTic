using CinemaTic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTic.Core.Utilities
{
    public static class Constants
    {
        public const string ImagesFolder = "client-images";
        public const string DateTimeFormat = "MM/dd/yyyy";

        public const int SectorRows = 10;
        public const int SectorCols = 15;
        public const string TrailerUrlRegex = "(.*?)(^|\\/|v=)([a-zA-Z0-9_-]{11})(.*)?";

        public const int ActorsPerPage = 5;
        public const int ActorMoviesPerPage = 5;
        public const int CinemasMoviesPerPage = 3;
        public const int TicketsPerPage = 5;
        public const int GenreMoviesPerPage = 5;
        public const int GenresPerPage = 10;
        public const int MoviesPerPage = 8;

        public const int Admin_CinemasPerPage = 10;
        public const int Admin_UsersPerPage = 8;
    }
}
