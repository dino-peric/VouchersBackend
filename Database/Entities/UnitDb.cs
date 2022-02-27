using System.ComponentModel.DataAnnotations;
using VouchersBackend.Models;

namespace VouchersBackend.Database;

public class UnitDb
{
    public UnitDb()
    {
        Vouchers = new HashSet<VoucherDb>();
    }

    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<VoucherDb>? Vouchers { get; set; }
}
