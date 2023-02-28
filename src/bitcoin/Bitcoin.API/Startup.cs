using Bitcoin.API.Filters;
using Bitcoin.Core.Interfaces;
using Bitcoin.Core.Services;
using Bitcoin.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Bitcoin.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //serilog configuration
            var seqUrl = Configuration["Seq:Url"];
            var seqKey = Configuration["Seq:Key"]; 
            var levelSwitch = new LoggingLevelSwitch();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (source, certificate, chain, sslPolicyError) => true;

            Log.Logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(Configuration)
                  .MinimumLevel.Debug()
                 .ReadFrom.Configuration(Configuration)
                  .MinimumLevel.Information()
                .WriteTo.Seq(seqUrl,
                             apiKey: seqKey,
                             controlLevelSwitch: levelSwitch,
                                messageHandler: httpClientHandler)
                .CreateLogger(); 

            //logging to be injected
            services.AddSingleton(Log.Logger);
             
            //Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new ClientMappingProfile());
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            //real db
            var conn = Configuration["ConnectionStrings:ShuldrzConnector"];
            var path = Configuration["Serilog:WriteTo:1:Args:Path"];
            Log.Information(conn);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IBitcoinCoreClient, BitcoinCoreClient>();
            services.AddTransient<ICoreLightningClient, CoreLightningClient>();
             
            services.AddScoped(typeof(UtilityService));
            //services.AddScoped(typeof(Services.CustomService));
            

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddDistributedMemoryCache();
            services.AddDataProtection();

            //HttpContext injection
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bitcoin API", Version = "v1" });
                //options.IncludeXmlComments(XmlCommentsFilePath);
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bitcoin API", Version = "v1" });
            //});

            services.AddMvc(o => { o.Filters.Add<GlobalExceptionFilter>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("ShuldrzCors"));
            //});

            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
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
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            //17 3 2021 - this is to allow static files to be plublicly accessible
            //file path now looks like this https://localhost:5001/Photos/Users/OIG-RKCSS4YWFKBH.png
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Docs")),
                RequestPath = "/Docs"
            });
            //Enable directory browsing
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "Docs")),
                RequestPath = "/Docs"
            });

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bitcoin API (v1)");
            });

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
