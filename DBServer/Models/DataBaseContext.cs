using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBServer
{
    /// <summary>
    /// Der Datenbankkontext der Anwendung, sprich das Modell der Datenbank 
    /// </summary>
    public class DataBaseContext : DbContext
    {
        /// <summary>
        /// Fügt die Tabelle Person dem Context hinzu
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Anbindung an die Datenbank personen.db bei Initialisierung 
        /// </summary>
        /// <param name="optionsBuilder">Eine Instanz von ObtionBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=./Models/personen.db");
        }
    }
}