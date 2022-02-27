using Microsoft.EntityFrameworkCore;
using VouchersBackend.Models;
using VouchersBackend.Database;
using System.Linq.Dynamic.Core;

namespace VouchersBackend.Repositores;

interface IWebshopRepository
{
    Task<List<WebshopDTO>> GetAllWebshops();
    Task<WebshopDTO?> GetWebshopById(int id);
    Task<WebshopDTO> CreateWebshop(CreateWebshopDTO newWebshop);
    Task<WebshopDTO?> UpdateWebshop(WebshopDTO updatedWebshop);
    Task<WebshopDTO?> DeleteWebshop(long id);
}

public class WebshopRepository : IWebshopRepository
{
    public async Task<List<WebshopDTO>> GetAllWebshops()
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            return await db.Webshops.Select(w => new WebshopDTO(w)).ToListAsync();
        }
    }

    public async Task<WebshopDTO?> GetWebshopById(int id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshop = await db.Webshops.Where(v => v.Id == id).FirstOrDefaultAsync();

            if (webshop == default(WebshopDb))
                return null;

            return new WebshopDTO(webshop);
        }
    }

    public async Task<WebshopDTO> CreateWebshop(CreateWebshopDTO newWebshop)
    {
        // TODO Validation
        // This is temporary hack just so it works, need to add proper validation later, need to see where
        //if (newVoucher.ValidTo is not null && newVoucher.ValidTo.Value.Kind == DateTimeKind.Unspecified)
        //    newVoucher.ValidTo = DateTime.SpecifyKind((DateTime)newVoucher.ValidTo, DateTimeKind.Utc);

        //if (newVoucher.ValidFrom.Kind == DateTimeKind.Unspecified)
        //    newVoucher.ValidFrom = DateTime.SpecifyKind(newVoucher.ValidFrom, DateTimeKind.Utc);
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshopDb = new WebshopDb(newWebshop);

            db.Webshops.Add(webshopDb);
            await db.SaveChangesAsync();

            return new WebshopDTO(webshopDb);
        }
    }


    public async Task<WebshopDTO?> UpdateWebshop(WebshopDTO updatedWebshop)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var webshop = await db.Webshops.FindAsync(updatedWebshop.Id);

            if (webshop == default(WebshopDb))
                return null;

            db.Entry(webshop).CurrentValues.SetValues(updatedWebshop);
            await db.SaveChangesAsync();

            return new WebshopDTO(webshop);
        }

    }

    public async Task<WebshopDTO?> DeleteWebshop(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Webshops.FindAsync(id) is WebshopDb voucherDb)
            {
                db.Webshops.Remove(voucherDb);
                await db.SaveChangesAsync();
                return new WebshopDTO(voucherDb);
            }

            return null; // Results.NotFound();
        }
    }
}