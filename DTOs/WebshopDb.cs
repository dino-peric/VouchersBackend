using System.ComponentModel.DataAnnotations;

namespace VouchersBackend.Models;

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
        Name = webshopDTO.Name;
        Url = webshopDTO.Url;
    }
}

public partial class WebshopDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public WebshopDTO() { }

    public WebshopDTO(int id, string name, string url)
    {
        Id = id;
        Name = name;
        Url = url;
    }

    public WebshopDTO(WebshopDb webshopDb)
    {
        Id = webshopDb.Id;
        Name = webshopDb.Name;
        Url = webshopDb.Url;
    }
}