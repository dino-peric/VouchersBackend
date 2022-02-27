using System.ComponentModel.DataAnnotations;
using VouchersBackend.Models;

namespace VouchersBackend.Database;

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
}