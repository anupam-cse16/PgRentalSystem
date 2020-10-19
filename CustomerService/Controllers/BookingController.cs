using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CustomerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepo _bookingRepository;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookingController));
        public BookingController(IBookingRepo bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
       /// <summary>
       /// Get Booking Details By Id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                _log4net.Info("Get BookingDetails by Id accessed");
                var bookinglist = _bookingRepository.GetById(id);
                return new OkObjectResult(bookinglist);
               
            }
            catch
            {
                _log4net.Error("Error in Getting Booking Details");
                return new NoContentResult();
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if(ModelState.IsValid)
                {
                
                   var bookobj= _bookingRepository.Book(booking);
                   // return Created("", booking);

                   return CreatedAtAction(nameof(Post), new { id = booking.BookingId }, booking);
                   
                }
                return BadRequest();

               
            }
            catch
            {
                _log4net.Error("Error in Adding Booking Details");
                return new NoContentResult();
            }
            
        }
    }
}
