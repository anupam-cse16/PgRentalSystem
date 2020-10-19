using Microsoft.EntityFrameworkCore;
using Pg_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pg_Service
{
    public class PgDbContext:DbContext
    {
     
    
        public PgDbContext(DbContextOptions<PgDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Pg> Pgs { get; set; }
      
    }
}
