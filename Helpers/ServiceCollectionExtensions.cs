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
        // TODO Unit tests?
        // TODO Use builder.environment
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        // connectionString = "Host=ec2-34-253-116-145.eu-west-1.compute.amazonaws.com:5432;Username=xlipplyaqjqvty;Password=7886ae47dadc320552c6c7dba28ff67f064be8a21af2dbb94f5f270c4a305961;Database=d96q92h3hnulcb";

        Console.WriteLine($"Hello im a connection string: {connectionString}");

        // connectionString = "Host=ec2-34-253-116-145.eu-west-1.compute.amazonaws.com:5432;Username=xlipplyaqjqvty;Password=7886ae47dadc320552c6c7dba28ff67f064be8a21af2dbb94f5f270c4a305961;Database=d96q92h3hnulcb";

        if (connectionString is null)
            throw new ArgumentNullException("Connection string not set! Set the \"CONNECTION_STRING\" env variable");

        // TODO see why line below does absolutely nothing in code, but "dotnet-ef migrations add InitialCreate" doesn't want to work without it 
        builder.Services.AddDbContext<VoucherdbContext>(options => options.UseNpgsql(connectionString));
        //// builder.Configuration.GetConnectionString("Default");

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

        // Find every assembly that implements IEndpointDefinition dynamically
        // TODO Make this explicit because this sucks
        builder.Services.AddModules(typeof(VoucherDb));

        builder.Services.AddEndpointsApiExplorer(); // Swagger docs
        builder.Services.AddSwaggerGen();

        return services;
    }
}