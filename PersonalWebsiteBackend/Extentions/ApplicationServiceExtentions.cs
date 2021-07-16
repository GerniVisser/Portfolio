using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtfolioBackend.BusinessLogic.Interfaces;
using ProtfolioBackend.BusinessLogic.Objects.Github;
using ProtfolioBackend.BusinessLogic.Processes.Github;
using ProtfolioBackend.Helpers;
using ProtfolioBackend.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    string pgHost = Environment.GetEnvironmentVariable("DB_Host");
                    string pgPort = Environment.GetEnvironmentVariable("DB_Port");
                    string pgUser = Environment.GetEnvironmentVariable("DB_Username");
                    string pgPass = Environment.GetEnvironmentVariable("DB_Password");
                    string pgDb = Environment.GetEnvironmentVariable("DB_Name");

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connStr);
            });

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddScoped<IGitHubSync, GithubSyncPO>();

            services.AddScoped<IUsers, GithubUserBO>();


            return services;
        }
    }
}
