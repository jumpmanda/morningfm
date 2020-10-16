using DnsClient.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MorningFM.Logic;
using MorningFM.Logic.DTOs;
using MorningFM.Logic.Repositories;
using MorningFM.Logic.Repository;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace MorningFM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();

            services.AddSingleton<MorningFMRepository<User>>((provider) =>{
                    return new MorningFMRepository<User>("mongodb://localhost:27017", "morningfm-library", "users");
            });
            services.AddSingleton<MorningFMRepository<Session>>((provider) => {
                return new MorningFMRepository<Session>("mongodb://localhost:27017", "morningfm-library", "sessions");
            });

            services.AddTransient<HttpHandler>();

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<SpotifyAuthorization>>();
            services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), logger);
            var logger2 = serviceProvider.GetService<ILogger<SpotifyHandler>>();
            services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), logger2);

            services.AddTransient<SpotifyHandler>((provider)=> {
                return new SpotifyHandler(provider.GetService<ILogger<SpotifyHandler>>(), provider.GetService<HttpHandler>());
            });

            services.AddTransient<SpotifyAuthorization>((provider) => { 
            return new SpotifyAuthorization(Configuration.GetValue<string>("Spotify:ClientId"), Configuration.GetValue<string>("Spotify:ClientSecret"), provider.GetService<ILogger<SpotifyAuthorization>>());
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
