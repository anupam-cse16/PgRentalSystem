using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pg_Service.Models
{
    public class Pg
    {
        [Key]
        public int pg_id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Rent_per_month { get; set; }
    }
}
