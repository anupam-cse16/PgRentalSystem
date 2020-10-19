using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Pg_Service;
using Pg_Service.Controllers;
using Pg_Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace PgTest
{
    public class Tests
    {
      
        List<Pg> pg = new List<Pg>();
        IQueryable<Pg> pgdata;
        Mock<DbSet<Pg>> mockSet;
        Mock<PgDbContext> pgcontextmock;
        [SetUp]
        public void Setup()
        {
            pg=new List<Pg>()
            {
                new Pg{pg_id = 1, Description="1BHK", Rent_per_month=100,Address="Place1"},
                  new Pg{pg_id = 2, Description="2BHK", Rent_per_month=200,Address="Place2"},
                    new Pg{pg_id = 3, Description="3BHK", Rent_per_month=400,Address="Place3"},
                      new Pg{pg_id = 4, Description="1BHK", Rent_per_month=700,Address="Place4"},

            };
           pgdata = pg.AsQueryable();
           mockSet = new Mock<DbSet<Pg>>();
            mockSet.As<IQueryable<Pg>>().Setup(m => m.Provider).Returns(pgdata.Provider);
            mockSet.As<IQueryable<Pg>>().Setup(m => m.Expression).Returns(pgdata.Expression);
            mockSet.As<IQueryable<Pg>>().Setup(m => m.ElementType).Returns(pgdata.ElementType);
            mockSet.As<IQueryable<Pg>>().Setup(m => m.GetEnumerator()).Returns(pgdata.GetEnumerator());
            var p = new DbContextOptions<PgDbContext>();
            pgcontextmock = new Mock<PgDbContext>(p);
            pgcontextmock.Setup(x => x.Pgs).Returns(mockSet.Object);
            

         
        }
     

        [Test]
        public void GetAllTest()
        {
            var pgrepo = new PgRepository(pgcontextmock.Object);
            var pglist = pgrepo.GetAll();
            Assert.AreEqual(4, pglist.Count());


      

        }
        [Test]
        public void GetByIdTest()
        {
            var pgrepo = new PgRepository(pgcontextmock.Object);
            var pgobj = pgrepo.GetById(2);
            Assert.IsNotNull(pgobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var pgrepo = new PgRepository(pgcontextmock.Object);
            var pgobj = pgrepo.GetById(88);
            Assert.IsNull(pgobj);
        }

    }

}
