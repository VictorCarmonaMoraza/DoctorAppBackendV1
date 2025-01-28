using Data.DBContext;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensiones
{
    public static class ServicioAplicacionExtension
    {
        public static IServiceCollection AgregarServiciosAplicacion(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
             {

                 option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description = "Ingresar Bearer [espacio] token \r\n\r|\n "+
                     "Ejemplo: Bearer ejoydfoifiosdfiousdoifu",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     //Type = SecuritySchemeType.ApiKey,
                     Scheme = "Bearer"
                 });
                 option.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
     });
             });

            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddCors();

            services.AddScoped<ITokenServicio, TokenServicio>();

            return services;
        }
    }
}
