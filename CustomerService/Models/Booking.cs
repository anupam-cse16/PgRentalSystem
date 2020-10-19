using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int pg_Id { get; set; }
      
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public int No_ofMonth { get; set; }
        public double TotalCost { get; set; }

    }
}
