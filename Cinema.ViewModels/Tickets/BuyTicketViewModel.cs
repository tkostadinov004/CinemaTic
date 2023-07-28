namespace Cinema.ViewModels.Tickets
{
    public class BuyTicketViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int RunningTime { get; set; }
        public string Director { get; set; }
        public DateTime ForDateTime { get; set; }
        public string CinemaName { get; set; }
        public int CinemaId { get; set; }
    }
}
