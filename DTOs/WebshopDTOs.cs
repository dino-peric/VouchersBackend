using System.ComponentModel.DataAnnotations;
using VouchersBackend.Database;

namespace VouchersBackend.Models;

public class WebshopDTO
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

public class CreateWebshopDTO
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public CreateWebshopDTO() { }

    public CreateWebshopDTO(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public CreateWebshopDTO(WebshopDb webshopDb)
    {
        Name = webshopDb.Name;
        Url = webshopDb.Url;
    }
}
