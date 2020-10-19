using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pg_Service.Models
{
    public class PgRepository:IPgRepository
    {
        private readonly PgDbContext pgDbContext;

        public PgRepository(PgDbContext pgDbContext)
        {
            this.pgDbContext = pgDbContext;
        }
        public  IEnumerable<Pg> GetAll()
        {
            var pglist = pgDbContext.Pgs.Include(r=>r).ToList();
            return pglist;
          //  return  pgDbContext.Pgs.ToList();
        }
        public  Pg GetById(int pg_id)
        {
            return pgDbContext.Pgs.FirstOrDefault(p => p.pg_id == pg_id);
        }
     
    }
}
