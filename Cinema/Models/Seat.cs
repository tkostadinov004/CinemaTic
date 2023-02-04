using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Utilities;

namespace Cinema.Models
{
    public class Seat
    {
        public Seat()
        {
            Price = CalculatePrice(SeatNumber);
        }

        [Key]
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsOccupied { get; set; } = false;

        private decimal MiddlePrice = 5;
        private decimal Difference = 0.2m;
        private decimal CalculatePrice(string seatNumber)
        {
            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            int middleRow = 10;
            int middleCol = 15;

            int rowDiff = Math.Abs(row - middleRow);
            int colDiff = Math.Abs(col - middleCol);

            decimal totalPrice = MiddlePrice - (Difference * rowDiff) - (Difference * colDiff);
            return totalPrice;
        }
    }
}
