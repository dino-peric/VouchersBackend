using System.ComponentModel.DataAnnotations;
using VouchersBackend.Database;
using AutoMapper;
namespace VouchersBackend.Models;

public class WebshopDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public WebshopDTO() { }
}

public class CreateWebshopDTO
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public CreateWebshopDTO() { }
}

public class WebshopProfile : Profile
{
    public WebshopProfile()
    {
        CreateMap<WebshopDTO, WebshopDb>().ReverseMap();
        CreateMap<CreateWebshopDTO, WebshopDb>().ReverseMap();
    }
}