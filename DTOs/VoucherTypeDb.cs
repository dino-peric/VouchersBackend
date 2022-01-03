using System.ComponentModel.DataAnnotations;
using VouchersBackend.Models;

namespace VouchersBackend;

public partial class VoucherTypeDb
{
    public VoucherTypeDb()
    {
        Vouchers = new HashSet<VoucherDb>();
    }
    
    [Key]
    public long Id { get; set; }
    public string Type { get; set; } = null!;

    public virtual ICollection<VoucherDb>? Vouchers { get; set; }

    public VoucherTypeDb(VoucherTypeDTO voucherTypeDTO)
    {
        Type = voucherTypeDTO.Type;
    }
}

public partial class VoucherTypeDTO
{
    public long Id { get; set; }
    public string Type { get; set; } = null!;

    public VoucherTypeDTO() { }

    public VoucherTypeDTO(VoucherTypeDb voucherDb)
    {
        Id = voucherDb.Id;
        Type = voucherDb.Type;
    }
}