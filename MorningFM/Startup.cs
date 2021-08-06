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
                string connection = "";
                if (!string.IsNullOrEmpty(Configuration["Mongo:domain"]) && Configuration["Mongo:domain"].Contains("localhost"))
                {
                    connection = $"mongodb://localhost:27017";
                }
                else
                {
                    connection = $"mongodb+srv://{Configuration["Mongo:user"]}:{Configuration["Mongo:password"]}@{Configuration["Mongo:domain"]}";
                }             
                    return new MorningFMRepository<User>(connection, 
                        Configuration["Mongo:database"], 
                        Configuration["Mongo:userCollection"]);
            });
            services.AddSingleton<MorningFMRepository<Session>>((provider) => {

                string connection = ""; 
                if (!string.IsNullOrEmpty(Configuration["Mongo:domain"]) && Configuration["Mongo:domain"].Contains("localhost"))
                {
                    connection = $"mongodb://localhost:27017";
                }
                else
                {
                    connection = $"mongodb+srv://{Configuration["Mongo:user"]}:{Configuration["Mongo:password"]}@{Configuration["Mongo:domain"]}";

                }
               
                return new MorningFMRepository<Session>(connection,
                        Configuration["Mongo:database"],
                        Configuration["Mongo:sessionCollection"]);
            });

            services.AddTransient<HttpHandler>();

            services.AddTransient<SpotifyHandler>((provider)=> {
                return new SpotifyHandler(provider.GetService<ILogger<SpotifyHandler>>(), provider.GetService<HttpHandler>());
            });

            services.AddTransient<SpotifyAuthorization>((provider) => { 
            return new SpotifyAuthorization(
                Configuration["Spotify:ClientId"],
                Configuration["Spotify:ClientSecret"],
                Configuration["Spotify:RedirectUri"], 
                provider.GetService<ILogger<SpotifyAuthorization>>());
            });

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 5001;                
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
