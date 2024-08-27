// ﻿using planck.API.Configuration;
// using planck.API.Middlewares;
// using planck.API.Service;
using Microsoft.OpenApi.Models;
using planck.API.Configuration;
using Serilog;

namespace planck.API
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServices(Configuration, typeof(IServiceInstaller).Assembly);
            // services.AddTransient<TraceMiddleware>();
            // services.AddTransient<GlobalExceptionMiddleware>();

            // Add services to the container.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            // builder.Host.UseSerilog();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // ConfigureMiddlewares(app);
        }

        // private void ConfigureMiddlewares(IApplicationBuilder app)
        // {
        //     app.UseMiddleware<TraceMiddleware>();
        //     app.UseMiddleware<GlobalExceptionMiddleware>();
        // }
    }
}