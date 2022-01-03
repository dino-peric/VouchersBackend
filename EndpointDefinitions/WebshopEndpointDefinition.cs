using VouchersBackend.Models;
using VouchersBackend.Repositores;

namespace VouchersBackend.EndpointDefinitions;

public class WebshopEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/webshops", GetAllWebshops);
        app.MapGet("/webshops/{id:int}", GetWebshopById);
        app.MapPost("/webshops", CreateWebshop);
        app.MapPut("/webshops/{id:int}", UpdateWebshop);
        app.MapDelete("/webshops/{id:int}", DeleteWebshop);
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

    internal async Task<IResult> CreateWebshop(IWebshopRepository repo, WebshopDTO webshop)
    {
        if (webshop == null)
            return Results.Problem("Provided voucher was null");

        var newWebshop = await repo.CreateWebshop(webshop);

        return Results.Created($"/vouchers/{newWebshop.Id}", newWebshop);
    }

    internal async Task<IResult> UpdateWebshop(IWebshopRepository repo, int id, WebshopDTO webshopTOUpdate)
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