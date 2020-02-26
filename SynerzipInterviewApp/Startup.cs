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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SynerzipInterviewApp.Models;
using SynerzipInterviewApp.Models.DataManager;
using SynerzipInterviewApp.Models.Repository;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;


//using AutoMapper;

namespace SynerzipInterviewApp
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

            //  services.Configure<CookiePolicyOptions>(options =>
            //  {
            //      // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //      options.CheckConsentNeeded = context => true;
            //      options.MinimumSameSitePolicy = SameSiteMode.None;
            //  });

            //  services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //.AddAzureAD(options => Configuration.Bind("AzureAd", options));
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme).
            //    AddAzureADBearer(options => Configuration.Bind("AzureAd", options));


            //  services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            //  {
            //      options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

            //      options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            //  });

            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:SynerzipInterviewDB"]));
            services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));
            services.Configure<AppConfig>(Configuration.GetSection("ConnectionString"));
            services.AddScoped<IAuthenticationRepository, LdapAuthenticationManager>();
            services.AddScoped<IInterviewRepository, InterviewManager>();
            services.AddScoped<IContentBlockRepository, ContentBlockManager>();


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JWTSettings.ValidIssuer,

                ValidateAudience = true,
                ValidAudience = JWTSettings.ValidAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JWTSettings.SigningKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = JWTSettings.ValidIssuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });
            
            services.AddSwaggerGen(c =>
            {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{ Title = "Core Api", Description = "Swagger Core Api" });

            });
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
         
            app.UseStatusCodePages();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Core Api");
            }
            );
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
