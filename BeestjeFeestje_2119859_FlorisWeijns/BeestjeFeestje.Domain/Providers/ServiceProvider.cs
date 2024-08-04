using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Domain.Services;
using BeestjeFeestje.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Providers
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IAnimalTypeService, AnimalTypeService>();
           
            return services;
        }
    }
}
