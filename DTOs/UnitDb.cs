using System.ComponentModel.DataAnnotations;
using VouchersBackend.Models;

namespace VouchersBackend;

public partial class UnitDb
{
    public UnitDb()
    {
        Vouchers = new HashSet<VoucherDb>();
    }

    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<VoucherDb>? Vouchers { get; set; }

    public UnitDb(UnitDTO unitDTO)
    {
        Name = unitDTO.Name;
    }
}

public partial class UnitDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public UnitDTO() { }

    public UnitDTO(UnitDb voucherDb)
    {
        Id = voucherDb.Id;
        Name = voucherDb.Name;
    }
}
