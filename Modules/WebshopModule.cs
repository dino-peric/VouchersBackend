using VouchersBackend.Models;
using VouchersBackend.Repositores;

namespace VouchersBackend.Modules;

public class WebshopModule : IModule
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/webshops", GetAllWebshops);
        app.MapGet("/webshops/{id:int}", GetWebshopById);
        app.MapPost("/webshops", CreateWebshop);
        app.MapPut("/webshops", UpdateWebshop);
        app.MapDelete("/webshops/{id:int}", DeleteWebshop);
    }

    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        WebshopRepository.ConfigureRepository(builder);
        return builder;
    }

    internal async Task<IResult> GetAllWebshops(IWebshopRepository repo)
    {
        var webshops = await repo.GetAllWebshops();
        return Results.Ok(webshops);
    }

    internal async Task<IResult> GetWebshopById(IWebshopRepository repo, int id)
    {
        var webshop = await repo.GetWebshopById(id);
        if (webshop is null)
            return Results.NotFound();

        return Results.Ok(webshop);
    }

    internal async Task<IResult> CreateWebshop(IWebshopRepository repo, CreateWebshopDTO webshop)
    {
        if (webshop == null)
            return Results.Problem("Provided voucher was null");

        var newWebshop = await repo.CreateWebshop(webshop);

        return Results.Created($"/webshops/{newWebshop.Id}", newWebshop);
    }

    internal async Task<IResult> UpdateWebshop(IWebshopRepository repo, WebshopDTO webshopTOUpdate)
    {
        var updatedWebshop = await repo.UpdateWebshop(webshopTOUpdate);

        if (updatedWebshop is null)
            return Results.NotFound();

        return Results.Ok(updatedWebshop);
    }

    internal async Task<IResult> DeleteWebshop(IWebshopRepository repo, int id)
    {
        var deleted = await repo.DeleteWebshop(id);

        if (deleted is null)
            return Results.NotFound();

        return Results.Ok(deleted);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IWebshopRepository, WebshopRepository>();
    }
}