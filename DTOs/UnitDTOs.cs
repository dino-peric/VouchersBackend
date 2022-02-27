using VouchersBackend.Database;

namespace VouchersBackend.Models;

public class UnitDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public UnitDTO() { }

    public UnitDTO(UnitDb unitDb)
    {
        Id = unitDb.Id;
        Name = unitDb.Name;
    }
}

public class CreateUnitDTO
{
    public string Name { get; set; } = null!;

    public CreateUnitDTO() { }

    public CreateUnitDTO(UnitDb unitDb)
    {
        Name = unitDb.Name;
    }
}