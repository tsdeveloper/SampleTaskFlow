using System.Diagnostics.CodeAnalysis;
using API.Extensions;
using Core.Entities;
using Infra.Seed;
using Microsoft.Extensions.Options;
using Serilog;

try
{

    var builder = WebApplication.CreateBuilder(args);


    builder.Host.AddSerilog(builder.Configuration, "Sample Audit");

    //Log.Information("Getting the motors running...");
    var configuration = builder.Configuration;

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowOrigins", policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerDocumentation();
    builder.Services.AddApplicationServices(configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    var appConfig = builder.Services.BuildServiceProvider().GetService<IOptions<AppConfig>>();

    if (appConfig.Value != null && appConfig.Value.EnabledUpdateMigration)
    {
        try
        {
            DbInitializer.InitDb(app, false);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex);
        }
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapControllers();

    app.UseCors("AllowOrigins");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}

[ExcludeFromCodeCoverage]
public partial class Program
{

}

[ExcludeFromCodeCoverage]
record Options(string value);