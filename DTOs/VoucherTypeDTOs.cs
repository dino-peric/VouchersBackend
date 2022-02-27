using System.ComponentModel.DataAnnotations;
using VouchersBackend.Database;
using AutoMapper;
namespace VouchersBackend.Models;

public class VoucherTypeDTO
{
    public long Id { get; set; }
    public string Type { get; set; } = null!;

    public VoucherTypeDTO() { }
}

public class CreateVoucherTypeDTO
{
    public string Type { get; set; } = null!;

    public CreateVoucherTypeDTO() { }
}


public class VoucherTypeProfile : Profile
{
    public VoucherTypeProfile()
    {
        CreateMap<VoucherTypeDTO, VoucherTypeDb>().ReverseMap();
        CreateMap<CreateVoucherTypeDTO, VoucherTypeDb>().ReverseMap();
    }
}