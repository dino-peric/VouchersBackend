using Microsoft.EntityFrameworkCore;
using VouchersBackend.Database;
using VouchersBackend.Models;

namespace VouchersBackend.Repositores;

interface IUnitRepository
{
    Task<List<UnitDTO>> GetAllUnits();
    Task<UnitDTO?> GetUnitById(int id);
    Task<UnitDTO> CreateUnit(CreateUnitDTO newUnit);
    Task<UnitDTO?> UpdateUnit(UnitDTO updatedUnit);
    Task<UnitDTO?> DeleteUnit(long id);
}

public class UnitRepository : IUnitRepository
{
    public async Task<List<UnitDTO>> GetAllUnits()
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            return await db.Units.Select(w => new UnitDTO(w)).ToListAsync();
        }
    }

    public async Task<UnitDTO?> GetUnitById(int id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unit = await db.Units.Where(v => v.Id == id).FirstOrDefaultAsync();

            if (unit == default(UnitDb))
                return null;

            return new UnitDTO(unit);
        }
    }

    public async Task<UnitDTO> CreateUnit(CreateUnitDTO newUnit)
    {
        // TODO Validation
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unitDb = new UnitDb(newUnit);

            db.Units.Add(unitDb);
            await db.SaveChangesAsync();

            return new UnitDTO(unitDb);
        }
    }

    public async Task<UnitDTO?> UpdateUnit(UnitDTO updatedUnit)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unitDb = await db.Units.FindAsync(updatedUnit.Id);

            if (unitDb == default(UnitDb))
                return null;

            db.Entry(unitDb).CurrentValues.SetValues(updatedUnit);
            await db.SaveChangesAsync();

            return new UnitDTO(unitDb);
        }
    }

    public async Task<UnitDTO?> DeleteUnit(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Units.FindAsync(id) is UnitDb voucherDb)
            {
                db.Units.Remove(voucherDb);
                await db.SaveChangesAsync();
                return new UnitDTO(voucherDb);
            }

            return null; // Results.NotFound();
        }
    }
}