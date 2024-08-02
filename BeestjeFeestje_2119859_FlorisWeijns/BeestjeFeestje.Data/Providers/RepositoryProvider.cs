using BeestjeFeestje.Data.Repositories;
using BeestjeFeestje.Data.Repositories.Interfaces;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Providers
{
    public static class RepositoryProvider
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IATypeRepository, ATypeRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IFarmRepository, FarmRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
