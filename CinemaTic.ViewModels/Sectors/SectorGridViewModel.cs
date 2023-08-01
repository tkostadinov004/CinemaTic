namespace CinemaTic.ViewModels.Sectors
{
    public class SectorGridViewModel
    {
        public int CinemaId { get; set; }
        public int ForMovieId { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public DateTime ForDateTime { get; set; }
        public SectorDetailsViewModel[] Sectors { get; set; }
    }
}
