using System.ComponentModel.DataAnnotations;
using VouchersBackend.Models;

namespace VouchersBackend.Database;

public partial class WebshopDb
{
    public WebshopDb()
    {
        Vouchers = new HashSet<VoucherDb>();
    }

    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public virtual ICollection<VoucherDb>? Vouchers { get; set; }

    public WebshopDb(WebshopDTO webshopDTO)
    {
        Id = webshopDTO.Id;
        Name = webshopDTO.Name;
        Url = webshopDTO.Url;
    }

    public WebshopDb(CreateWebshopDTO createWebshopDTO)
    {
        Name = createWebshopDTO.Name;
        Url = createWebshopDTO.Url;
    }
}