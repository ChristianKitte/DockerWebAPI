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
    [Route("api/v1/[controller]/")]
    public class PersonsController : ControllerBase
    {
        private DataBaseContext _context;

        public PersonsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetAll()
        {
            try
            {
                var person = _context.Persons.Select(x => x).ToArray();
                HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return Ok(person.ToList());
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        //gibt ein Objekt zurück
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetByID(int id)
        {
            try
            {
                var persons = _context.Persons.FirstOrDefault(x => x.PersonID == id);
                return Ok(persons);
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        //fügt ein Objekt hinzu
        [HttpPost("{vorname}/{nachname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult AddPerson(string vorname, string nachname)
        {
            try
            {
                var person = new Person();
                person.Vorname = vorname;
                person.Nachname = nachname;

                _context.Persons.Add(person);
                _context.SaveChanges();

                return Ok(person);
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        //ändert ein Objekt
        [HttpPut("{id}/{vorname}/{nachname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult UpdatePerson(int id, string vorname, string nachname)
        {
            try
            {
                var person = _context.Persons.FirstOrDefault(x => x.PersonID == id);
                person.Vorname = vorname;
                person.Nachname = nachname;

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        //löscht ein Objekt
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                var person = _context.Persons.Where(x => x.PersonID == id).FirstOrDefault();
                _context.Persons.Remove(person);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        //löscht alle Objekte
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeletePerson()
        {
            try
            {
                var persons = _context.Persons.Select(x => x);
                foreach (var person in persons)
                {
                    _context.Persons.Remove(person);
                }

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception f)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }
    }
}