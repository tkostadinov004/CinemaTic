using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class Seat
    {
        public Seat(string seatNumber)
        {
            Price = CalculatePrice(seatNumber);
            Sector = DefineSector(seatNumber);
            SeatNumber = seatNumber;
        }
        [Key]
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price {get; private set;}
        public string Sector { get; private set; }
        public bool IsOccupied { get; set; } = false;

        private decimal MiddlePrice = 5;
        private decimal Difference = 0.2m;
        private decimal CalculatePrice(string seatNumber)
        {
            var sn = seatNumber[1..3];

            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            int middleRow = 11;
            int middleCol = 16;

            int rowDiff = Math.Abs(row - middleRow);
            int colDiff = Math.Abs(col - middleCol);

            decimal totalPrice = MiddlePrice - (Difference * rowDiff) - (Difference * colDiff);
            return totalPrice;
        }
        private string DefineSector(string seatNumber)
        {
            int row = int.Parse(seatNumber[1..3]);
            int col = int.Parse(seatNumber[4..]);

            string sector = "";
            if (row < Constants.HallRows / 2)
            {
                if (col <= Constants.HallCols / 3)
                {
                    sector = "A";
                }
                else if(col > Constants.HallCols / 3 && col <= (Constants.HallCols / 3) * 2)
                {
                    sector = "B";
                }
                else
                {
                    sector = "C";
                }
            }
            else
            {
                if (col <= Constants.HallCols / 3)
                {
                    sector = "D";
                }
                else if (col > Constants.HallCols / 3 && col <= (Constants.HallCols / 3) * 2)
                {
                    sector = "E";
                }
                else
                {
                    sector = "F";
                }
            }
            return sector;
        }
    }
}
