using VouchersBackend.Modules;

namespace VouchersBackend.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapEndpoints();
        app.UseCors();

        return app;
    }
}
