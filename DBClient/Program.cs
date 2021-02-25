using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DBClient
{
    /// <summary>
    /// Kapselt das eigentliche Programm 
    /// </summary>
    class Program
    {
        /// <summary>
        /// Der einzige HttpClient
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Startroutine der Anwendung. Zum Testen des DBServers werden im Verlauf der Funktion alle Methoden
        /// aufgerufen und das Ergebnis ausgegeben. 
        /// </summary>
        /// <returns>Der asynchrone Task</returns>
        private static async Task Main()
        {
            Console.WriteLine("Starte...");
            Console.WriteLine("");

            Console.WriteLine("Lösche alles Personen...");
            await deleteAllePersonen();
            Console.WriteLine("");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            Console.WriteLine("");

            Console.WriteLine("Füge Person Alpha Beta hinzu...");
            Person newPerson = await addPerson("Alpha", "Beta");
            Console.WriteLine(newPerson.ToString());
            Console.WriteLine("");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            Console.WriteLine("");

            Console.WriteLine(String.Format("Hole einzige Person mit ID {0}...",
                new string[] {newPerson.PersonID.ToString()}));
            await getPersonByID(newPerson.PersonID);
            Console.WriteLine("");

            Console.WriteLine(String.Format("Ändere einzige Person mit ID {0}...",
                new string[] {newPerson.PersonID.ToString()}));
            await updatePerson(newPerson.PersonID, newPerson.Vorname + " - Geändert",
                newPerson.Nachname + " - Geändert");
            Console.WriteLine("");

            Console.WriteLine(String.Format("Hole einzige Person mit ID {0}...",
                new string[] {newPerson.PersonID.ToString()}));
            await getPersonByID(newPerson.PersonID);
            Console.WriteLine("");

            Console.WriteLine(String.Format("Lösche einzige Person mit ID {0}...",
                new string[] {newPerson.PersonID.ToString()}));
            await deletePersonByID(newPerson.PersonID);
            Console.WriteLine("");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            Console.WriteLine("");

            Console.WriteLine("Fertig!");
        }

        /// <summary>
        /// Holt alle Entitäten und gibt sie aus
        /// </summary>
        /// <returns>Der Task</returns>
        private static async Task getAllePersonen()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // erster nicht so guter Versuch
            // var responseTask = client.GetStringAsync("http://localhost:8080/api/v1/persons/");
            // var responseString = await responseTask;

            List<Person> persons =
                await client.GetFromJsonAsync<List<Person>>("http://localhost:8080/api/v1/persons/");

            if (persons != null && persons.Count > 0)
            {
                Console.WriteLine("Liste aller Personen:");
                foreach (var person in persons)
                {
                    Console.WriteLine(person.ToString());
                }
            }
            else
            {
                {
                    Console.WriteLine("Keine Personen vorhanden...");
                }
            }
        }

        /// <summary>
        /// Holt eine einzelne Entität und gibt sie aus
        /// </summary>
        /// <param name="id">Die ID der zu holenden Entität</param>
        /// <returns>Der Task</returns>
        private static async Task getPersonByID(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Person person =
                await client.GetFromJsonAsync<Person>("http://localhost:8080/api/v1/persons/" + id.ToString());

            Console.WriteLine(person.ToString());
        }

        /// <summary>
        /// Fügt eine neue Entität zu 
        /// </summary>
        /// <param name="vorname">Der Vorname</param>
        /// <param name="nachname">Der Nachname</param>
        /// <returns>Die neu hinzugefügte Entität</returns>
        private static async Task<Person> addPerson(string vorname, string nachname)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Galante Möglichkeit mit Fluent API, bei POST ein JSON zu erhalten
            var responseTask = await
                client.PostAsync("http://localhost:8080/api/v1/persons/" + vorname + "/" + nachname, null).Result
                    .Content.ReadFromJsonAsync(typeof(Person));

            return (Person) responseTask;
        }

        /// <summary>
        /// Aktualisiert eine Entität
        /// </summary>
        /// <param name="id">Die feste, eindeutige ID der zur aktualisierenden Entität</param>
        /// <param name="vorname">Der neue Vorname</param>
        /// <param name="nachname">Der neue Nachname</param>
        /// <returns>Der Task</returns>
        private static async Task updatePerson(int id, string vorname, string nachname)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var responseTask = await
                client.PutAsync(
                    "http://localhost:8080/api/v1/persons/" + id.ToString() + "/" + vorname + "/" +
                    nachname, null);
        }

        /// <summary>
        /// Löscht alle vorhandenen Entitäten
        /// </summary>
        /// <returns>Der Task</returns>
        private static async Task deleteAllePersonen()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var responseTask = await client.DeleteAsync("http://localhost:8080/api/v1/persons/");
        }

        /// <summary>
        /// Löscht eine einzelne Entität
        /// </summary>
        /// <param name="id">Die ID der zu löschenden Entität</param>
        /// <returns>Der Task</returns>
        private static async Task deletePersonByID(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var responseTask = await
                client.DeleteAsync("http://localhost:8080/api/v1/persons/" + id.ToString());
        }
    }
}