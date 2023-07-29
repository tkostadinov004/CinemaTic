using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public static class Constants
    {
        public const int HallRows = 21;
        public const int HallCols = 31;
        public static Dictionary<string, Tuple<Coords, Coords>> SectorBorderValues = new Dictionary<string, Tuple<Coords, Coords>>
        {
            {"A", new Tuple<Coords, Coords>(new Coords{Row =  1, Col = 1},  new Coords{Row= 9, Col = 10 })},
            {"B", new Tuple<Coords, Coords>(new Coords{Row =  1, Col = 11},  new Coords{Row= 9, Col = 20 })},
            {"C", new Tuple<Coords, Coords>(new Coords{Row =  1, Col = 21},  new Coords{Row= 9, Col = 31 })},
            {"D", new Tuple<Coords, Coords>(new Coords{Row =  10, Col = 1},  new Coords{Row= 21, Col = 10 })},
            {"E", new Tuple<Coords, Coords>(new Coords{Row =  10, Col = 11},  new Coords{Row= 21, Col = 20 })},
            {"F", new Tuple<Coords, Coords>(new Coords{Row =  10, Col = 21},  new Coords{Row= 21, Col = 31 })},
        };

        public const string ImagesFolder = "client-images";
        public const string DateTimeFormat = "MM/dd/yyyy";

        public const int SectorRows = 10;
        public const int SectorCols = 10;
        public const string TrailerUrlRegex = "(.*?)(^|\\/|v=)([a-zA-Z0-9_-]{11})(.*)?";
    }
}
