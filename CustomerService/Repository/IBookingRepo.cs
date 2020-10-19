using CustomerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService
{
     public interface IBookingRepo
    {
        public IEnumerable<Booking> GetById(int userid);
        
        Booking Book(Booking entity);
    }
}
