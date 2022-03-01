using VouchersBackend.Models;
using VouchersBackend.Database;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VouchersBackend.Modules;

namespace VouchersBackend.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        Console.WriteLine($"Hello im a connection string: {connectionString}");

        if (connectionString is null)
            throw new ArgumentNullException("Connection string not set! Set the \"CONNECTION_STRING\" env variable");

        // TODO see why line below does absolutely nothing in code, but "dotnet-ef migrations add InitialCreate" doesn't want to work without it 
        builder.Services.AddDbContext<VoucherdbContext>(options => options.UseNpgsql(connectionString));

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("*");
                });
        });

        builder.Services.AddEndpointsApiExplorer(); // Swagger docs
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(Program));

        // builder.Services.AddModules(typeof(VoucherDb));
        builder.AddModules(typeof(VoucherDb));


        return services;
    }
}