using WalletApp.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

using Serilog;
using WalletApp.Server.Domain;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt",
    rollingInterval: RollingInterval.Day,
    rollOnFileSizeLimit: true)
    .CreateLogger();

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(configuration);
builder.Services.Configure<ConfigSettings>(builder.Configuration);

builder.Logging.AddSerilog();
builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddWalletServices();

builder.Services.AddDbContext<WalletContext>(opt =>
    opt.UseInMemoryDatabase("WalletList"));

builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

builder.WebHost.UseUrls(urls: builder.Configuration[nameof(ConfigSettings.WEB_HOST_URL)]!);

var app = builder.Build();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();