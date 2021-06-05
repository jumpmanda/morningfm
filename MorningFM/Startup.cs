using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MorningFM.Logic;
using MorningFM.Logic.DTOs;
using MorningFM.Logic.Repository;
using MorningFM.Middleware;

namespace MorningFM
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
            services.AddSwaggerGen();
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();

            services.AddTokenAuthentication(Configuration);

            services.AddSingleton<MorningFMRepository<User>>((provider) =>{
                    return new MorningFMRepository<User>(Configuration.GetSection("Mongo:connection").Value, 
                        Configuration.GetSection("Mongo:database").Value, 
                        Configuration.GetSection("Mongo:userCollection").Value);
            });
            services.AddSingleton<MorningFMRepository<Session>>((provider) => {
                return new MorningFMRepository<Session>(Configuration.GetSection("Mongo:connection").Value,
                        Configuration.GetSection("Mongo:database").Value,
                        Configuration.GetSection("Mongo:sessionCollection").Value);
            });

            services.AddTransient<HttpHandler>();

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();            

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
                       
            app.UseRouting();            
            app.UseAuthentication();
            app.UseAuthorization();
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
