using Microsoft.EntityFrameworkCore;
using VouchersBackend.Database;
using VouchersBackend.Models;
using AutoMapper;

namespace VouchersBackend.Repositores;

interface IUnitRepository
{
    Task<List<UnitDTO>> GetAllUnits();
    Task<UnitDTO?> GetUnitById(int id);
    Task<UnitDTO?> GetUnitByName(string name);

    Task<UnitDTO> CreateUnit(CreateUnitDTO newUnit);
    Task<UnitDTO?> UpdateUnit(UnitDTO updatedUnit);
    Task<UnitDTO?> DeleteUnit(long id);

    Task<bool> UnitExists(string name);
    Task<bool> UnitExists(UnitDTO unit);
    Task<bool> UnitExists(CreateUnitDTO unit);
}

public class UnitRepository : IUnitRepository
{
    private static IMapper _mapper = null!;

    public static void ConfigureRepository(WebApplicationBuilder builder)
    {
        var provider = builder.Services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>(); 
    }

    public async Task<List<UnitDTO>> GetAllUnits()
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            return await db.Units.Select(u => _mapper.Map<UnitDTO>(u)).ToListAsync();
        }
    }

    public async Task<UnitDTO?> GetUnitById(int id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unit = await db.Units.Where(v => v.Id == id).FirstOrDefaultAsync();

            if (unit == default(UnitDb))
                return null;

            return _mapper.Map<UnitDTO>(unit);
        }
    }

    public async Task<UnitDTO?> GetUnitByName(string name)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unit = await db.Units.Where(u => u.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefaultAsync();

            if (unit == default(UnitDb))
                return null;

            return _mapper.Map<UnitDTO>(unit);
        }
    }

    public async Task<UnitDTO> CreateUnit(CreateUnitDTO newUnit)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            var unitDb = _mapper.Map<UnitDb>(newUnit);

            db.Units.Add(unitDb);
            await db.SaveChangesAsync();

            return _mapper.Map<UnitDTO>(unitDb);
        }
    }

    public async Task<bool> UnitExists(string name)
    {
        if (name == "" || name == null)
            return false;

        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Units.AnyAsync(u => u.Name.Trim().ToLower() == name.Trim().ToLower()))
                return true;

            return false;
        }
    }

    public async Task<bool> UnitExists(UnitDTO unit)
    {
        return await UnitExists(unit.Name);
    }

    public async Task<bool> UnitExists(CreateUnitDTO unit)
    {
        return await UnitExists(unit.Name);
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

            return _mapper.Map<UnitDTO>(unitDb);
        }
    }

    public async Task<UnitDTO?> DeleteUnit(long id)
    {
        using (VoucherdbContext db = new VoucherdbContext())
        {
            if (await db.Units.FindAsync(id) is UnitDb unitDb)
            {
                db.Units.Remove(unitDb);
                await db.SaveChangesAsync();
                return _mapper.Map<UnitDTO>(unitDb);
            }

            return null; // Results.NotFound();
        }
    }

    // TODO This cannot be translated to pure SQL, figure out something later (invariant collation or something)
    private bool compareUnitNames(string name1, string name2)
    {
        if (name1.Trim().ToLower() == name2.Trim().ToLower())
            return true;

        return false;
    }
}