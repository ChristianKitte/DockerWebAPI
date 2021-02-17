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

        [HttpGet("api/v1/get_persons")]
        public IActionResult Get()
        {
            var persons = _context.Persons.Select(x => x).ToArray();
            return Ok(persons);
        }

        [HttpGet("api/v1/get_person_by_id/{id}")]
        public IActionResult Get(int id)
        {
            var persons = _context.Persons.FirstOrDefault(x => x.PersonID == id);
            return Ok(persons);
        }
        
        [HttpPut("api/v1/add_person/{vorname}/{nachname}")]
        public IActionResult Put(string vorname, string nachname)
        {
            var person = new Person();
            person.Vorname = vorname;
            person.Nachname = nachname;

            _context.Persons.Add(person);
            _context.SaveChanges();

            return Ok(person);
        }
        
        [HttpPost("api/v1/update_person/{id}/{vorname}/{nachname}")]
        public IActionResult Post(int id,string vorname, string nachname)
        {
            var person = _context.Persons.FirstOrDefault(x=>x.PersonID==id);
            person.Vorname = vorname;
            person.Nachname = nachname;

            _context.SaveChanges();

            return Ok(person);
        }

        [HttpDelete("api/v1/delete_person/{id}")]
        public IActionResult Delete(int id)
        {
            var person = _context.Persons.Where(x => x.PersonID == id).FirstOrDefault();
            _context.Persons.Remove(person);
            _context.SaveChanges();

            return NoContent();
        }
    }
}