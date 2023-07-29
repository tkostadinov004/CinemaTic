using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Seeder.DTOs.New
{
    public class AddCinemaDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty("founding_date")]
        public string FoundingDate { get; set; }
        public string Rows { get; set; }
        public string Columns { get; set; }
        public string Image { get; set; }
    }
}
