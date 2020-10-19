using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PgClientApplication.Models
{
    public class Pg
    {
        public int pg_id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Rent_per_month { get; set; }
    }
}
