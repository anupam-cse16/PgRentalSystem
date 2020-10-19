using CustomerService;
using CustomerService.Controllers;
using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace CustomerServiceTest
{
    public class Tests
    {

        List<Booking> books = new List<Booking>();
        IQueryable<Booking> bookingdata;
        Mock<DbSet<Booking>> mockSet;
        Mock<BookingDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            books = new List<Booking>()
            {
                new Booking{BookingId = 1, pg_Id=1,UserId=1,BookingDate=DateTime.Parse("15-10-2020 00:00:00"),No_ofMonth =1,TotalCost=6000.00},
                 new Booking{BookingId = 2, pg_Id=1,UserId=1,BookingDate=DateTime.Parse("15-10-2020 00:00:00"),No_ofMonth =2,TotalCost=6000.00}

            };
            bookingdata = books.AsQueryable();
            mockSet = new Mock<DbSet<Booking>>();
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(bookingdata.Provider);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(bookingdata.Expression);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(bookingdata.ElementType);
            mockSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(bookingdata.GetEnumerator());
            var p = new DbContextOptions<BookingDbContext>();
            bookcontextmock = new Mock<BookingDbContext>(p);
            bookcontextmock.Setup(x => x.Bookings).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllBookingsByUserIdTest()
        {
            var bookingrepo = new BookingRepo(bookcontextmock.Object);
            var bookinglist = bookingrepo.GetById(1);
            Assert.AreEqual(2, bookinglist.Count());




        }
        [Test]
        public void AddBookingDetailTest()
        {
            var bookingrepo = new BookingRepo(bookcontextmock.Object);
            var bookingobj = bookingrepo.Book(new Booking { BookingId = 3, pg_Id = 1, UserId = 1, BookingDate = DateTime.Parse("15-10-2020 00:00:00"), No_ofMonth = 1, TotalCost = 6000.00 });
            Assert.IsNotNull(bookingobj);
        }
    }
}