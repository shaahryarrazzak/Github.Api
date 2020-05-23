using Git.Data.Service;
using Git.Data.Service.Entity;
using Git.Data.Service.Service;
using Git.Data.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Git.Data.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGitHttpService, GitHttpService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddSingleton<IRepoService, RepoService>();
            services.AddSingleton<GitSyncWorker>();
            services.AddSingleton<ConcurrentDictionary<int, GitRepo>>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetService<GitSyncWorker>();
        }
    }
}
