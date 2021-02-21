using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starte...");
            Console.WriteLine("");
            
            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            Console.WriteLine("");

            Console.WriteLine("Hole Person mit ID 5...");
            await getPersonByID(5);
            Console.WriteLine("");

            Console.WriteLine("Füge Person Alpha Beta hinzu...");
            await addPerson("Alpha", "Beta");
            Console.WriteLine("");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            Console.WriteLine("");
            
            Console.WriteLine("Fertig!");
        }

        private static async Task getAllePersonen()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask = client.GetStringAsync("http://localhost:8080/api/v1/persons/");
            var responseString = await responseTask;

            List<Person> persons =
                await client.GetFromJsonAsync<List<Person>>("http://localhost:8080/api/v1/persons/");

            Console.WriteLine("Liste aller Personen:");
            foreach (var person in persons)
            {
                Console.WriteLine(String.Format("Person mit der ID {0} und dem Namen {1} {2} wurde geholt",
                    new String[] {person.PersonID.ToString(), person.Vorname, person.Nachname}));
            }
        }

        private static async Task getPersonByID(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Person person =
                await client.GetFromJsonAsync<Person>("http://localhost:8080/api/v1/persons/" + id.ToString());

            Console.WriteLine(String.Format("Person mit der ID {0} und dem Namen {1} {2} wurde geholt",
                new String[] {person.PersonID.ToString(), person.Vorname, person.Nachname}));
        }

        private static async Task addPerson(string vorname, string nachname)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask =
                client.PostAsync("http://localhost:8080/api/v1/persons/" + vorname + "/" + nachname, null);
            var responseString = await responseTask.Result.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
        }
    }
}