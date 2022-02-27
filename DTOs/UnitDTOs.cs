using VouchersBackend.Database;
using AutoMapper;
namespace VouchersBackend.Models;

public class UnitDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;

    public UnitDTO() { }
}

public class CreateUnitDTO
{
    public string Name { get; set; } = null!;

    public CreateUnitDTO() { }
}

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitDTO, UnitDb>().ReverseMap();
        CreateMap<CreateUnitDTO, UnitDb>().ReverseMap();
    }
}