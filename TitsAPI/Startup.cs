using System.IO;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.AutoMapperProfiles;
using Services.Implementations;

namespace TitsAPI
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string WWWRootPath { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Error =
                            (sender, args) => _logger.LogCritical(args.ErrorContext.Error.Message);
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }
                );

            // DbContext will take connection string from Environment or throw
            services.AddDbContext<TitsDbContext>();

            // Add Repositories
            services.AddScoped<ICourierTokenSessionRepository, CourierTokenSessionRepository>();
            services.AddScoped<IManagerTokenSessionRepository, ManagerTokenSessionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICourierAccountRepository, CourierAccountRepository>();
            services.AddScoped<IManagerAccountRepository, ManagerAccountRepository>();
            services.AddScoped<IWorkerRoleRepository, WorkerRoleRepository>();
            services.AddScoped<IWorkerToRoleRepository, WorkerToRoleRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<ILatLngRepository, LatLngRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<ICourierSessionRepository, CourierSessionRepository>();
            services.AddScoped<ICourierMessageRepository, CourierMessageRepository>();
            services.AddScoped<ISosRequestRepository, SosRequestRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();

            // Add Services
            services.AddScoped<ITokenSessionService, TokenSessionService>();
            services.AddScoped<ICourierAccountService, CourierAccountService>();
            services.AddScoped<IManagerAccountService, ManagerAccountService>();
            services.AddScoped<IAccountRoleService, AccountRoleService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICourierSessionService, CourierSessionService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IMessagingService, MessagingService>();
            services.AddScoped<ISosService, SosService>();
            services.AddScoped<IZoneService, ZoneService>();
            
            services.AddSingleton<IAutoDeliveryServerService, AutoDeliveryServerService>();

            services.AddAutoMapper(cfg => cfg.AddProfile(new TitsAutomapperProfile()));

            services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo() {Title = "TITS Swagger"}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My First Swagger");
                c.RoutePrefix = "docs";
            });

            WWWRootPath = Path.GetFullPath("../TITS_front", env.ContentRootPath);

            // app.UseHttpsRedirection();

            _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogInformation(WWWRootPath);

            env.WebRootFileProvider = new PhysicalFileProvider(WWWRootPath);

            app.UseDefaultFiles(); // Serve index.html for route "/"
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = env.WebRootFileProvider,
                ServeUnknownFileTypes = true
            });

            app.UseRouting();

            app.UseCors(builder => builder
                .WithOrigins(
                    "http://localhost:8080",
                    "http://localhost:4200",
                    "https://localhost:8443",
                    "https://localhost:4200",
                    "http://akiana.io:8080",
                    "https://akiana.io:8443")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "api_area",
                    areaName: "API",
                    pattern: "api/{controller}/{action}");

                endpoints.MapDefaultControllerRoute();
            });
            
            // serve index.html for everything not mapped
            app.UseSpa(builder => { builder.Options.SourcePath = WWWRootPath; });

            var autoDeliveryServerService = app.ApplicationServices.GetRequiredService<IAutoDeliveryServerService>();

            autoDeliveryServerService.GetMode(1);
        }
    }
}