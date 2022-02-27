using VouchersBackend.Repositores;
using VouchersBackend.Models;

namespace VouchersBackend.Modules;

public class UnitModule : IModule
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/units", GetAllUnits);
        app.MapGet("/units/{id:int}", GetUnitById);
        app.MapPost("/units", CreateUnit);
        app.MapPut("/units", UpdateUnit);
        app.MapDelete("/units/{id:int}", DeleteUnit);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IUnitRepository, UnitRepository>();
    }

    public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder)
    {
        UnitRepository.ConfigureRepository(builder);
        return builder;
    }

    internal async Task<IResult> GetAllUnits(IUnitRepository repo)
    {
        var webshops = await repo.GetAllUnits();
        return Results.Ok(webshops);
    }

    internal async Task<IResult> GetUnitById(IUnitRepository repo, int id)
    {
        var unit = await repo.GetUnitById(id);
        if (unit is null)
            return Results.NotFound();

        return Results.Ok(unit);
    }

    internal async Task<IResult> CreateUnit(IUnitRepository repo, CreateUnitDTO unit)
    {
        if (unit == null)
            return Results.Problem("Provided voucher was null");

        var newUnit = await repo.CreateUnit(unit);

        return Results.Created($"/units/{newUnit.Id}", newUnit);
    }

    internal async Task<IResult> UpdateUnit(IUnitRepository repo, UnitDTO unitToUpdate)
    {
        var updatedUnit = await repo.UpdateUnit(unitToUpdate);

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
}