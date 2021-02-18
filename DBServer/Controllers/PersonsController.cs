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
    [Route("[controller]/api/v1")]
    public class PersonsController : ControllerBase
    {
        private DataBaseContext _context;

        public PersonsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var person = _context.Persons.Select(x => x).ToArray();
                return Ok(person.ToList());
            }
            catch (Exception f)
            {
                return Ok(f);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
           var persons = _context.Persons.FirstOrDefault(x => x.PersonID == id);
           return Ok(persons);
        }
        
        [HttpPut("{vorname}/{nachname}")]
        public IActionResult AddPerson(string vorname, string nachname)
        {
            var person = new Person();
            person.Vorname = vorname;
            person.Nachname = nachname;

            _context.Persons.Add(person);
            _context.SaveChanges();

            return Ok(person);
        }
        
        [HttpPost("{id}/{vorname}/{nachname}")]
        public IActionResult UpdatePerson(int id,string vorname, string nachname)
        {
            var person = _context.Persons.FirstOrDefault(x=>x.PersonID==id);
            person.Vorname = vorname;
            person.Nachname = nachname;

            _context.SaveChanges();

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var person = _context.Persons.Where(x => x.PersonID == id).FirstOrDefault();
            _context.Persons.Remove(person);
            _context.SaveChanges();

            return NoContent();
        }
    }
}