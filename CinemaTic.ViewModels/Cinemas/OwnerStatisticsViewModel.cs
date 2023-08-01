using CinemaTic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTic.ViewModels.Cinemas
{
    public class OwnerStatisticsViewModel
    {
        public decimal PersonalIncome { get; set; }
        public decimal TotalIncome { get; set; }
        public IDictionary<string, int> CinemasCustomers { get; set; }
        public IDictionary<string, decimal> CinemasTotalRevenues { get; set; }
    }
}
