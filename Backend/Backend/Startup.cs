using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Backend.Exceptions;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Data;
using Backend.Mappings;
using Backend.Models;
using Backend.Models.Entities;
using Backend.Providers;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using React.AspNet;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ErrorResponses = new VersionErrorResponseProvider();
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            var jwtAppOptions = Configuration.GetSection("JwtOptions");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppOptions["Key"]));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppOptions["Issuer"];
                options.Audience = jwtAppOptions["Audience"];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                options.ValidFor = TimeSpan.FromDays(Convert.ToDouble(jwtAppOptions["ExpireDays"]));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAppOptions["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwtAppOptions["Audience"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSingleton<IJwtService, JwtService>();
            
            services.AddAutoMapper(typeof(ViewModelToEntityProfile), typeof(EntityToViewModelProfile));
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddMvc();
            services.AddCors();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            return services.BuildServiceProvider();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(build => {
                build.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var exception = error.Error;
                        var code = HttpStatusCode.InternalServerError;

                        if (exception is NotFoundException) code = HttpStatusCode.NotFound;
                        else if (exception is ArgumentException) code = HttpStatusCode.BadRequest;

                        var result = JsonConvert.SerializeObject(new { Message = exception.Message });

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)code;

                        await context.Response.WriteAsync(result);
                    }
                });
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Test}/{action=Index}/{id?}");
            });
            app.UseAuthentication();
            

            app.UseReact(config => { });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }

    }
}
