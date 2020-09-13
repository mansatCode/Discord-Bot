using DiscordBot.Bots;
using DiscordBot.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DiscordBot.Bots
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RPGContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RPGContext;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("DiscordBot.DAL.Migrations"));
            });

            var serviceProvider = services.BuildServiceProvider();
            var bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
