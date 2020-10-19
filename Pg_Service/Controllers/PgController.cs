using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pg_Service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pg_Service.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PgController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PgController));
        private readonly IPgRepository _pgRepository;
        public PgController(IPgRepository pgRepository)
        {
            _pgRepository = pgRepository;
        }
        // GET: api/<PgController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                IEnumerable<Pg> pglist = _pgRepository.GetAll();
                return Ok(pglist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info("Http Get by Id is accessed");
                var pglist = _pgRepository.GetById(id);
                return new OkObjectResult(pglist);
            }
            catch
            {
                _log4net.Error("Error in Get by id Request");
                return new NoContentResult();
            }
        }

    }
}
