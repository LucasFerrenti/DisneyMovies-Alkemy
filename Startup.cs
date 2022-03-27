using Alkemy.Models;
using Alkemy.Models.Common;
using Alkemy.Repository;
using Alkemy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alkemy
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
            services.AddRazorPages();

            //cors
            services.AddCors(o => o.AddPolicy("Alow All", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            //Inject DbContext
            services.AddDbContext<DisneyMoviesContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DisneyMoviesDB"),
                options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            );

            //Configure AppSettings
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //Configure JWT Auth
            var JwtKey = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings").Get<AppSettings>().JwtKey);

            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(JwtKey),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });
            //Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("active", policy => policy.RequireClaim("confirm"));
                options.AddPolicy("user", policy => policy.RequireClaim("user"));
            });
            //Repositories
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICharactersRepository, CharactersRespository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            //Services
            services.AddScoped<IAuthService, JwtAuthService>();
            services.AddScoped<IEmailService, NetEmailService>();
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

            app.UseCors("Alow All");
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Character}/{ action = Get}");
            });
        }
    }
}
