using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DBClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        
        private static async Task Main(string[] args)
        {
            // 1) Alle Personen löschen
            // 2) Neue Person anlegen
            // 3) Neue Person anzeigen
            // 4) Neue Perosn ändern
            // 5) Neue Person anzeigen
                                      
            Console.WriteLine("Starte...");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();
            
            Console.WriteLine("Füge Person Alpha Beta hinzu...");
            await addPerson("Alpha","Beta");

            Console.WriteLine("Hole alle Personen...");
            await getAllePersonen();

            Console.WriteLine("Hole Person mit ID 5...");
            await getPersonByID(5);

            Console.WriteLine("Ändere Person mit ID 5...");
            //await getPersonByID(5);

            Console.WriteLine("Fertig!");
        }

        private static async Task getAllePersonen()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask = client.GetStringAsync("http://localhost:8080/persons/api/v1/");
            var responseString = await responseTask;
            
            Console.WriteLine(responseString);
        }

        private static async Task addPerson(string vorname, string nachname)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask = client.PutAsync("http://localhost:8080/persons/api/v1/" + vorname + "/" + nachname,null);
            var responseString = await responseTask;

            Console.WriteLine(responseString);
        }
        
        private static async Task getPersonByID(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseTask = client.GetStringAsync("http://localhost:8080/persons/api/v1/" + id.ToString());
            var responseString = await responseTask;

            Console.WriteLine(responseString);
        }
    }
}
