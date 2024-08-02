using BeestjeFeestje.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Providers
{
    public static class DatabaseProvider
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add db contexts
            services.AddDbContextPool<BeestjeFeestjeDBContext>(opt =>
                opt.UseSqlServer(connectionString, b =>
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery).MigrationsAssembly("BeestjeFeestje.Data")));

            return services;
        }
    }
}
