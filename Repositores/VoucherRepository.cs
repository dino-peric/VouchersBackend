using Microsoft.EntityFrameworkCore;
using VouchersBackend.Helpers;
using VouchersBackend.Models;
using VouchersBackend.Database;
using System.Linq.Dynamic.Core;

namespace VouchersBackend.Repositores;
interface IVoucherRepository
{
    Task<List<VoucherDTO>> GetAllVouchers();
    Task<VoucherDTO?> GetVoucherById(long id);
    Task<VoucherDTO> CreateVoucher(VoucherDTO newVoucher);
    Task<VoucherDTO?> UpdateVoucher(VoucherDTO voucher);
    Task<VoucherDTO?> DeleteVoucher(long id);

    Task<VoucherDTO?> UpvoteVoucher(long id);
    Task<VoucherDTO?> DownvoteVoucher(long id);

    Task<List<VoucherDTO>> QueryVouchers(SortingParameter sortBy, SortingDirection dir, string filter, uint pageNumber, uint pageSize);

    Task<List<VoucherTypeDTO>> GetAllVoucherTypes();
    Task<VoucherTypeDTO?> GetVoucherTypeById(int id);
    Task<VoucherTypeDTO> CreateVoucherType(VoucherTypeDTO newVoucher);
    Task<VoucherTypeDTO?> UpdateVoucherType(VoucherTypeDTO voucher);
    Task<VoucherTypeDTO?> DeleteVoucherType(long id);
}

public class VoucherRepository : IVoucherRepository
{
    #region Vouchers
    public async Task<List<VoucherDTO>> QueryVouchers(SortingParameter sortBy,
                                                      SortingDirection sortDir,
                                                      string filter,
                                                      uint pageNumber,
                                                      uint pageSize)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            IQueryable<VoucherDb> query = db.Vouchers;

            if (filter != "")   // TODO make search smarter, use tsvector
                query = query.Where(q => q.Webshop.Name.ToLower().Contains(filter.ToLower()));

            string orderByClause = sortBy.ToDbQueryString() + " " + sortDir.ToDbQueryString();
            query = query.OrderBy(orderByClause);

            return await query
                .Include(v => v.Webshop)    // Todo these includes might need to be implicit. Not sure yet.
                .Include(v => v.Unit)
                .Include(v => v.Type)
                .Skip((int)((pageNumber - 1) * pageSize))
                .Take((int)pageSize)
                .Select(v => new VoucherDTO(v, true, true, true)).ToListAsync();
        }
    }

    public async Task<List<VoucherDTO>> GetAllVouchers()
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            return await db.Vouchers
                    .Include(v => v.Webshop)
                    .Include(v => v.Type)
                    .Include(v => v.Unit)
                    .Select(v => new VoucherDTO(v, true, true, true))
                    .ToListAsync();
        }
    }

    public async Task<VoucherDTO?> GetVoucherById(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var voucher = await db.Vouchers
                .Include(v => v.Webshop)
                .Include(v => v.Type)
                .Include(v => v.Unit)
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();

            if (voucher == default(VoucherDb))
                return null;

            return new VoucherDTO(voucher, true, true, true);
        }
        // if (await db.Vouchers.FindAsync(id) is VoucherDb voucherDb)
        // {
        //     return new VoucherDTO(voucherDb, true, true, true); // TODO Need to see how to implicitly get webshop, type, unit without doing .Include(v => v.webshop)
        // }

        // return null; // Not Found
    }

    public async Task<VoucherDTO> CreateVoucher(VoucherDTO newVoucher)
    {
        // TODO Validation
        // This is temporary hack just so it works, need to add proper validation later, need to see where
        //if (newVoucher.ValidTo is not null && newVoucher.ValidTo.Value.Kind == DateTimeKind.Unspecified)
        //    newVoucher.ValidTo = DateTime.SpecifyKind((DateTime)newVoucher.ValidTo, DateTimeKind.Utc);

        using (VoucherdbContext db = new VoucherdbContext())
        {
            var voucherDb = new VoucherDb(newVoucher);

            db.Add(voucherDb);
            await db.SaveChangesAsync();
            return new VoucherDTO(voucherDb);
        }
    }

    public async Task<VoucherDTO?> UpdateVoucher(VoucherDTO updatedVoucher)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Vouchers.FindAsync(updatedVoucher.Id) is VoucherDb voucherDb)
            {
                db.Entry(voucherDb).CurrentValues.SetValues(updatedVoucher);
                await db.SaveChangesAsync();
                return new VoucherDTO(voucherDb);
            }

            return null; // Not Found
        }
    }

    public async Task<VoucherDTO?> DeleteVoucher(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Vouchers.FindAsync(id) is VoucherDb voucherDb)
            {
                db.Vouchers.Remove(voucherDb);
                await db.SaveChangesAsync();
                return new VoucherDTO(voucherDb);
            }

            return null; // Not Found
        }
    }

    public async Task<VoucherDTO?> UpvoteVoucher(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Vouchers.FindAsync(id) is VoucherDb voucherDb)
            {
                voucherDb.Likes++;
                await db.SaveChangesAsync();
                return new VoucherDTO(voucherDb);
            }

            return null; // Not Found
        }
    }

    public async Task<VoucherDTO?> DownvoteVoucher(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Vouchers.FindAsync(id) is VoucherDb voucherDb)
            {
                voucherDb.Dislikes++;
                await db.SaveChangesAsync();
                return new VoucherDTO(voucherDb);
            }

            return null; // Not Found
        }
    }
    #endregion
    #region VoucherTypes
    public async Task<List<VoucherTypeDTO>> GetAllVoucherTypes()
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            return await db.VoucherTypes.Select(w => new VoucherTypeDTO(w)).ToListAsync();
        }
    }

    public async Task<VoucherTypeDTO?> GetVoucherTypeById(int id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshop = await db.VoucherTypes.Where(v => v.Id == id).FirstOrDefaultAsync();

            if (webshop == default(VoucherTypeDb))
                return null;

            return new VoucherTypeDTO(webshop);
        }
    }

    public async Task<VoucherTypeDTO> CreateVoucherType(VoucherTypeDTO newVoucherType)
    {
        // TODO Validation
        // This is temporary hack just so it works, need to add proper validation later, need to see where
        //if (newVoucher.ValidTo is not null && newVoucher.ValidTo.Value.Kind == DateTimeKind.Unspecified)
        //    newVoucher.ValidTo = DateTime.SpecifyKind((DateTime)newVoucher.ValidTo, DateTimeKind.Utc);

        //if (newVoucher.ValidFrom.Kind == DateTimeKind.Unspecified)
        //    newVoucher.ValidFrom = DateTime.SpecifyKind(newVoucher.ValidFrom, DateTimeKind.Utc);
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshopDb = new VoucherTypeDb(newVoucherType);

            db.VoucherTypes.Add(webshopDb);
            await db.SaveChangesAsync();

            return new VoucherTypeDTO(webshopDb);
        }
    }

    public async Task<VoucherTypeDTO?> UpdateVoucherType(VoucherTypeDTO updatedVoucherType)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshop = await db.VoucherTypes.FindAsync(updatedVoucherType.Id);

            if (webshop == default(VoucherTypeDb))
                return null;

            db.Entry(webshop).CurrentValues.SetValues(updatedVoucherType);
            await db.SaveChangesAsync();

            return new VoucherTypeDTO(webshop);
        }
    }

    public async Task<VoucherTypeDTO?> DeleteVoucherType(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.VoucherTypes.FindAsync(id) is VoucherTypeDb voucherDb)
            {
                db.VoucherTypes.Remove(voucherDb);
                await db.SaveChangesAsync();
                return new VoucherTypeDTO(voucherDb);
            }

            return null; // Results.NotFound();
        }
    }
    #endregion
}
