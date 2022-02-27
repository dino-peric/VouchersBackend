using VouchersBackend.Helpers;
using VouchersBackend.Models;
using VouchersBackend.Repositores;

namespace VouchersBackend.Modules;

public class VoucherModule : IModule
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/vouchers", GetAllVouchers)
           .WithName("GetAllVouchers")
           .Produces<List<VoucherDTO>>();
        
        app.MapGet("/vouchers/{id:int}", GetVoucherById)
           .WithName("GetVoucherById")
           .ProducesProblem(404)
           .Produces<VoucherDTO>();

        app.MapPost("/vouchers", CreateVoucher)
           .ProducesProblem(500)
           .Produces<VoucherDTO>();

        app.MapPut("/vouchers", UpdateVoucher);


        app.MapDelete("/vouchers/{id:int}", DeleteVoucher);
        app.MapPost("/vouchers/like/{id:int}", UpvoteVoucher);
        app.MapPost("/vouchers/dislike/{id:int}", DownvoteVoucher);
        app.MapGet("/vouchers/query", QueryVouchers);

        app.MapGet("/vouchertypes", GetAllVoucherTypes);
        app.MapGet("/vouchertypes/{id:int}", GetVoucherTypeById);
        app.MapPost("/vouchertypes", CreateVoucherType);
        app.MapPut("/vouchertypes", UpdateVoucherType);
        app.MapDelete("/vouchertypes/{id:int}", DeleteVoucherType);

    }

    #region Vouchers
    internal async Task<IResult> QueryVouchers(IVoucherRepository repo,
                                               SortingParameter sortBy = SortingParameter.Popularity,
                                               SortingDirection dir = SortingDirection.Descending,
                                               string filter = "",
                                               uint pageNumber = 1,
                                               uint pageSize = 25)
    {
        var vouchers = await repo.QueryVouchers(sortBy, dir, filter, pageNumber, pageSize);
        return Results.Ok(vouchers);
    }

    internal async Task<IResult> GetAllVouchers(IVoucherRepository repo)
    {
        var vouchers = await repo.GetAllVouchers();
        return Results.Ok(vouchers);
    }

    internal async Task<IResult> GetVoucherById(IVoucherRepository repo, int id)
    {
        var voucher = await repo.GetVoucherById(id);
        if (voucher is null)
            return Results.NotFound();

        return Results.Ok(voucher);
    }

    internal async Task<IResult> CreateVoucher(IVoucherRepository repo, CreateVoucherDTO voucher)
    {
        if (voucher == null)
            return Results.Problem("Provided voucher was null");

        var newVoucher = await repo.CreateVoucher(voucher);
        // return Results.Created($"/vouchers/{newVoucher.Id}", newVoucher);
         return Results.CreatedAtRoute("GetVoucherById", new { id = newVoucher.Id }, newVoucher);
    }

    internal async Task<IResult> UpdateVoucher(IVoucherRepository repo, UpdateVoucherDTO voucherToUpdate)
    {
        var updatedVoucher = await repo.UpdateVoucher(voucherToUpdate);

        if (updatedVoucher is null)
            return Results.NotFound();

        // return Results.Ok(updatedVoucher);
        return Results.NoContent();
    }

    internal async Task<IResult> DeleteVoucher(IVoucherRepository repo, int id)
    {
        var deleted = await repo.DeleteVoucher(id);

        if (deleted is null)
            return Results.NotFound();

        // return Results.Ok(deleted);
        return Results.NoContent();
    }

    internal async Task<IResult> UpvoteVoucher(IVoucherRepository repo, int id)
    {
        var newVoucher = await repo.UpvoteVoucher(id);

        if (newVoucher is null)
            return Results.NotFound();

        return Results.Ok(newVoucher.Popularity);
    }

    internal async Task<IResult> DownvoteVoucher(IVoucherRepository repo, int id)
    {
        var newVoucher = await repo.DownvoteVoucher(id);

        if (newVoucher is null)
            return Results.NotFound();

        return Results.Ok(newVoucher.Popularity);
    }
    #endregion

    #region VoucherTypes
    internal async Task<IResult> GetAllVoucherTypes(IVoucherRepository repo)
    {
        var voucherTypes = await repo.GetAllVoucherTypes();
        return Results.Ok(voucherTypes);
    }

    internal async Task<IResult> GetVoucherTypeById(IVoucherRepository repo, int id)
    {
        var voucherType = await repo.GetVoucherTypeById(id);
        if (voucherType is null)
            return Results.NotFound();

        return Results.Ok(voucherType);
    }

    internal async Task<IResult> CreateVoucherType(IVoucherRepository repo, CreateVoucherTypeDTO voucherType)
    {
        if (voucherType == null)
            return Results.Problem("Provided voucher was null");

        var newVoucherType = await repo.CreateVoucherType(voucherType);

        return Results.Created($"/voucherTypes/{newVoucherType.Id}", newVoucherType);
    }

    internal async Task<IResult> UpdateVoucherType(IVoucherRepository repo, VoucherTypeDTO voucherTypeToUpdate)
    {
        var updatedVoucherType = await repo.UpdateVoucherType(voucherTypeToUpdate);

        if (updatedVoucherType is null)
            return Results.NotFound();

        return Results.Ok(updatedVoucherType);
    }

    internal async Task<IResult> DeleteVoucherType(IVoucherRepository repo, int id)
    {
        var deleted = await repo.DeleteVoucherType(id);

        if (deleted is null)
            return Results.NotFound();

        return Results.Ok(deleted);
    }
    #endregion
    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IVoucherRepository, VoucherRepository>();
    }
}