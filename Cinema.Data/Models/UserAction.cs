using Cinema.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Models
{
    public class UserAction
    {
        public Guid Id { get; set; } = new Guid();
        public UserActionType Type { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
    }
}
