using VouchersBackend.Repositores;

namespace VouchersBackend.EndpointDefinitions;

public class UnitEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/units", GetAllUnits);
        app.MapGet("/units/{id:int}", GetUnitById);
        app.MapPost("/units", CreateUnit);
        app.MapPut("/units/{id:int}", UpdateUnit);
        app.MapDelete("/units/{id:int}", DeleteUnit);
    }

    internal async Task<IResult> GetAllUnits(IUnitRepository repo)
    {
        var webshops = await repo.GetAllUnits();
        return Results.Ok(webshops);
    }

    internal async Task<IResult> GetUnitById(IUnitRepository repo, int id)
    {
        var webshop = await repo.GetUnitById(id);
        if (webshop is null)
            return Results.NotFound();

        return Results.Ok(webshop);
    }

    internal async Task<IResult> CreateUnit(IUnitRepository repo, UnitDTO webshop)
    {
        if (webshop == null)
            return Results.Problem("Provided voucher was null");

        var newUnit = await repo.CreateUnit(webshop);

        return Results.Created($"/vouchers/{newUnit.Id}", newUnit);
    }

    internal async Task<IResult> UpdateUnit(IUnitRepository repo, int id, UnitDTO webshopTOUpdate)
    {
        var updatedUnit = await repo.UpdateUnit(webshopTOUpdate);

        if (updatedUnit is null)
            return Results.NotFound();

        return Results.Ok(updatedUnit);
    }

    internal async Task<IResult> DeleteUnit(IUnitRepository repo, long id)
    {
        var deleted = await repo.DeleteUnit(id);

        if (deleted is null)
            return Results.NotFound();

        return Results.Ok(deleted);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IUnitRepository, UnitRepository>();
    }
}