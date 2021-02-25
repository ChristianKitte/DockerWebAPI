using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer
{
    /// <summary>
    /// Programmstart
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Einstiegsmethode Main 
        /// </summary>
        /// <param name="args">Parameterliste</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Erstellung eines HostBuilder Objektes
        /// </summary>
        /// <param name="args">Parameterliste</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}