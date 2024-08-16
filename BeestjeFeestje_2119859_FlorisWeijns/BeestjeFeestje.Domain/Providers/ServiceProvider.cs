using BeestjeFeestje.Domain.Services;
using BeestjeFeestje.Domain.Services.Interfaces;
using BeestjeFeestje.Domain.Services.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BeestjeFeestje.Domain.Providers
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IAnimalTypeService, AnimalTypeService>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddTransient<IBookingValidator, PredatorValidator>();
            services.AddTransient<IBookingValidator, DesertAnimalValidator>();
            services.AddTransient<IBookingValidator, ArcticAnimalValidator>();
            services.AddTransient<IBookingValidator, AnimalAmountValidator>();
            services.AddTransient<IBookingValidator, VipValidator>();

            return services;
        }
    }
}
