using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        public int Vorname { get; set; }
        [Required]
        public int Nachname { get; set; }
    }
}
