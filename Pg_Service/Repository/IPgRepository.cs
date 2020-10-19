using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pg_Service.Models
{
    public interface IPgRepository
    {
        IEnumerable<Pg> GetAll();
        Pg GetById(int pgid);
   


    }
}
