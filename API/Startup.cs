using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Extensions;
using API.Infterfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration config)
        {
            _config = config;
           
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // Se le llama Dependency Injection Containes y contiene los servicios que
        // se quieren usar (Conexion a la Base de Datos/DbContex, seguridad, etc)
        public void ConfigureServices(IServiceCollection services)
        {
            
            // Servicios: Token y Base de Datos
            services.AddApplicationServices(_config);

            services.AddControllers();

            // Este servicio de seguridad para el mecanismo de seguridad CORS
            // Impide/bloquea que se llamen servicios con origen distinto.
            // En este caso es localhost:4200 (Angular) llama a localhost:5001 (API)
            services.AddCors();

            // Servicio Autenticacion 
            services.AddIdentityServices(_config);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // El servicio CORS debe ir justo aqui, entre UseRouting y UseAuthorization
            // Permite cualquier metodo con origen en el request localhost/4200
            app.UseCors( x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            // Se añade la autenticacion justo antes de la autorizacion
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
