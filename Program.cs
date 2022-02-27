using VouchersBackend.Api.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.GetName().Name,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development
});

builder.Services.AddApplicationServices(builder);

var app = builder.Build();
app.ConfigureApplication();

// connectionString = "Host=ec2-34-253-116-145.eu-west-1.compute.amazonaws.com:5432;Username=xlipplyaqjqvty;Password=7886ae47dadc320552c6c7dba28ff67f064be8a21af2dbb94f5f270c4a305961;Database=d96q92h3hnulcb";

app.Run($"http://localhost:{Environment.GetEnvironmentVariable("PORT") ?? throw new ArgumentNullException("Port not set! Set the \"PORT\" env variable")}");