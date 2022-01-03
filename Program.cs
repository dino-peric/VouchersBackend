using VouchersBackend.Models;
using VouchersBackend.Database;
using VouchersBackend.EndpointDefinitions;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// TODO Dockerize
// TODO Add ENV variable for database connection string
// TODO Unit tests?

// TODO see why line below does absolutely nothing in code, but "dotnet-ef migrations add InitialCreate" doesn't want to work without it 
var connectionString = "Host=localhost:5432;Username=postgres;Password=internet;Database=migrationstest";
builder.Services.AddDbContext<VoucherdbContext>(options => options.UseNpgsql(connectionString));

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Find every assembly that implements IEndpointDefinition dynamically
// TODO Make this explicit because this sucks
builder.Services.AddEndpointDefinitions(typeof(VoucherDb));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseEndpointDefinitions();

app.UseSwagger();
app.UseSwaggerUI();

app.Run("http://localhost:3000");