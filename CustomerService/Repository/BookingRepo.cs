using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService
{
    public class BookingRepo:IBookingRepo
    {
        private readonly BookingDbContext bookingDbContext;

        public BookingRepo(BookingDbContext bookingDbContext)
        {
            this.bookingDbContext = bookingDbContext;
        }
       
     //   public Booking GetById(int userid)
             public IEnumerable<Booking> GetById(int userid)
        {

           // list = _context.Carts.Where(c => c.UserId == id).ToList<Cart>();

            return  bookingDbContext.Bookings.Where(b => b.UserId==userid).ToList<Booking>();
           // return await BookingDbContext.Bookings.FirstOrDefaultAsync(b=b. == pg_id);
        }
        public Booking Book(Booking booking)
        {
          

                var result = bookingDbContext.Bookings.Add(booking);

                bookingDbContext.SaveChanges();
            return booking;
            
        
           // return result.Entity;

        }

    }
}
