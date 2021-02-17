using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection

{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            //services.AddScoped<IStudentRepository, StudentRepository>();
            //services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
