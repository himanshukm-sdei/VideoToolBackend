using System;

using System.Text;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using AutoMapper;
using Domain.Entities;
using ElmahCore.Sql;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using Stripe;

namespace VisionAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private string DefaultCorsPolicyName;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            });
            services.AddRazorPages();
            services.AddDbContext<therapistContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBCNSTR")));


            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );
        
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSetting = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSetting.Key);
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DBCNSTR");
            });
            services.AddCors(o => o.AddPolicy("VisionAppPolicy", builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(new string[] { "http://localhost:4200" });
            }));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #region
            //Dependancy injection resolver
            //Repository
            services.AddScoped<IAuthRepo, AuthRepo>();
            services.AddScoped<IPractitionerRepo, PractitionerRepo>();
            services.AddScoped<ISettingsRepo, SettingsRepo>();
            services.AddScoped<ICompanyRepo, CompanyRepo>();
            services.AddScoped<ISuperAdminRepo, SuperAdminRepo>();
            services.AddScoped<IJobsRepo, JobsRepo>();
            services.AddScoped<ICommonEmailsRepo, CommonEmailsRepo>();
            services.AddScoped<IVideoRepo, VideoRepo>();
            //Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IPractitionerService, PractitionerService>();
            services.AddScoped<ICommonEmailsService, CommonEmailsService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<ISuperAdminService, SuperAdminnService>();
            services.AddScoped<IJobsService, JobsService>();
            services.AddScoped<IVideoService, VideoService>();


            services.AddAutoMapper(typeof(Startup));
            //Mapper.CreateMap<YourEntityViewModel, YourEntity>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("AppSettings")["StripeSecretKey"]);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseHttpsRedirection();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //Path.Combine(env.ContentRootPath, "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});
            app.UseStaticFiles();
            app.UseRouting();
            app.UseElmah();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
