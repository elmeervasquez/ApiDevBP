

using ApiDevBP.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Host.UseSerilog();

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
