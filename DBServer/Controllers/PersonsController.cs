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
    /// <summary>
    /// Der Controller für die Entität Person (Tabelle Person in der Datenbank)
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class PersonsController : ControllerBase
    {
        /// <summary>
        /// Hält den für den Controller gültige DBContext
        /// </summary>
        private DataBaseContext _context;

        /// <summary>
        /// Der für den Controller gültige DBContext
        /// </summary>
        /// <param name="context">Ein gültiger Controller</param>
        public PersonsController(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Liefert alle verfügbare Personen 
        /// </summary>
        /// <returns>OK und eine Liste von Instanzen der hinzugefügten Entität</returns>
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
            catch (Exception)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        /// <summary>
        /// Liefert die durch die übergebene ID definierte Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OK und eine Instanz der Entität mit der übergebenen ID oder Forbid</returns>
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
            catch (Exception)
            {
                //HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        /// <summary>
        /// Fügt eine neue Person mit den übergebenen Daten hinzu
        /// </summary>
        /// <param name="vorname">Der Vorname</param>
        /// <param name="nachname">Der Nachname</param>
        /// <returns>OK und eine Instanz der hinzugefügten Entität oder Forbid</returns>
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
            catch (Exception)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        /// <summary>
        /// Aktualisiert die durch die ID bestimmte Person. Die ID ist nicht änderbar.
        /// </summary>
        /// <param name="id">Die ID der Person</param>
        /// <param name="vorname">Der Vorname</param>
        /// <param name="nachname">Der Nachname</param>
        /// <returns>NoContent</returns>
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
            catch (Exception)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        /// <summary>
        /// Löscht die durch die ID bestimmte Person aus der Datenbank
        /// </summary>
        /// <param name="id">Die ID der Person</param>
        /// <returns>NoContent</returns>
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
            catch (Exception)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }

        /// <summary>
        /// Löschte alle Personen aus der Datenbank
        /// </summary>
        /// <returns>NoContent</returns>
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
            catch (Exception)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Forbid();
            }
        }
    }
}