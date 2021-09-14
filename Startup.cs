using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using storyshare_backend_dotnet_v3.Models;
using Microsoft.EntityFrameworkCore;

namespace storyshare_backend_dotnet_v3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy"

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
              opt.AddPolicy(name: _policyName, builder =>
              {
                builder.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
                });
              });

            services.AddControllers();

            // Save connection string from appsettings.json into variable
            var connectionString = Configuration["DBContextSettings:ConnectionString"];
            // Register db context as service
            services.AddDbContext<StoryContext>(opt => opt.UseNpgsql(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "storyshare_backend_dotnet_v3", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "storyshare_backend_dotnet_v3 v1"));
            }

            // forces stie to redirect to https, reinstate for heroku deploy
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_policyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Enable pattern matching
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{URLParam?}");
            });
        }
    }
}
