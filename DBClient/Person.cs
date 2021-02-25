using System;
using System.ComponentModel.DataAnnotations;

namespace DBClient
{
    /// <summary>
    /// Beschreibt eine Person 
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Die ID der Person 
        /// </summary>
        [Key]
        public int PersonID { get; set; }

        /// <summary>
        /// Der Vorname der Person                  
        /// </summary>
        [Required]
        public string Vorname { get; set; }

        /// <summary>
        /// Der Nachname der Person 
        /// </summary>
        [Required]
        public string Nachname { get; set; }

        /// <summary>
        /// Überschreibt die ToString() Methode
        /// </summary>
        /// <returns>Einen String, der die Entität beschreibt</returns>
        public override string ToString()
        {
            return String.Format("Person mit der ID {0} und dem Namen {1} {2} wurde geholt",
                new String[] {this.PersonID.ToString(), this.Vorname, this.Nachname});
        }
    }
}