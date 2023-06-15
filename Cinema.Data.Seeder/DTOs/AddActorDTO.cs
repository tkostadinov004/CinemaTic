using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Seeder.DTOs
{
    public class AddActorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Nationality { get; set; }
        public string IMDBRating { get; set; }
    }
}
