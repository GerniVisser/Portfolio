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
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddScoped<IGitHubSync, GithubSyncPO>();

            services.AddScoped<IUsers, GithubUserBO>();


            return services;
        }
    }
}
