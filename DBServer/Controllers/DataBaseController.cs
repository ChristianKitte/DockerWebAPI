using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DBServer;
using Microsoft.AspNetCore.Http;

namespace DBServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        private PersonenContext _context;

        public DataBaseController(PersonenContext context)
        {
            _context = context;
        }

        [HttpGet("api/v1/[controller]")]
        public IActionResult Get()
        {
            var persons = _context.Persons.Select(x => x).ToArray();
            return Ok(persons);
        }

        [HttpGet]
        [HttpGet("api/v1/[controller]{id}")]
        public IActionResult Get(int id)
        {
            var persons = _context.Persons.Where(x => x.PersonID == id).FirstOrDefault();
            return Ok(persons);
        }
    }
}