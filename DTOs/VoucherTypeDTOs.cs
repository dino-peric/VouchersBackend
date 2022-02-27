using System.ComponentModel.DataAnnotations;
using VouchersBackend.Database;

namespace VouchersBackend.Models;

public class VoucherTypeDTO
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

public class CreateVoucherTypeDTO
{
    public string Type { get; set; } = null!;

    public CreateVoucherTypeDTO() { }

    public CreateVoucherTypeDTO(VoucherTypeDb voucherDb)
    {
        Type = voucherDb.Type;
    }
}