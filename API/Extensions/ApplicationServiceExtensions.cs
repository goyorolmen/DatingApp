using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Infterfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    // Cuando se crean Extensions, las clases son estaticas ya que no
    // es necesario instanciarlas. Simplemente se usan.
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Servicion Token
            // Se usa AddScoped porque se elimina cuando el servicio no se usa mas
            services.AddScoped<ITokenService, TokenService>();
            // Añadir la conexion a la Base de Datos
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));    
            });

            return services;
        }
    }
}