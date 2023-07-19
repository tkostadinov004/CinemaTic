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
        public string ForDateTime { get; set; }
        public int CinemaId { get; set; }
        public string Time { get; set; }
    }
}
