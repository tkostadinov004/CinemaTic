using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public class CinemaDTO
    {
        [JsonProperty("id")]
        public int CinemaId { get; set; }
        [JsonProperty("fromDate")]
        public DateTime FromDate { get; set; }
        [JsonProperty("toDate")]
        public DateTime ToDate { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
