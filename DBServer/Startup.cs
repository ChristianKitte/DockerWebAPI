using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DBServer
{
    /// <summary>
    /// Der eigentliche Startpunkt der Anwendung. Konfiguration der Service und Middleware
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Diese Methode wird von der Runtime aufgerufen. Hier werden Services zum Container hinzugefügt.
        /// Die Unterstützung für Controller oder einen DB Context wird hier konfiguriert.
        /// </summary>
        /// <param name="services">Eine Instanz von ServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>();
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DBServer", Version = "v1"}); });
            services.AddDbContext<DataBaseContext>();
        }

        /// <summary>
        /// Diese Methode wird von der Runtime aufgerufen. Hier wird die HTTP request pipeline konfiguriert.
        /// Middleware wie Routing Https Redirections werden hier konfiguriert.
        /// </summary>
        /// <param name="app">Eine Instanz von ApplictaionBuilder</param>
        /// <param name="env">Eine Instanz von WebHostEnvironment</param>                                         
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DBServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}