using VouchersBackend.Database;
using System.Text.Json.Serialization;
using AutoMapper;

namespace VouchersBackend.Models;

public class VoucherDTO
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? Code { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public int Popularity { get { return (int)(Likes - Dislikes); } }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WebshopDTO? Webshop { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UnitDTO? Unit { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VoucherTypeDTO? Type { get; set; } = null!;

    public VoucherDTO() { }
}

public class CreateVoucherDTO
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? Code { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }

    public long WebshopId { get; set; }
    public long UnitId { get; set; }
    public long TypeId { get; set; }

    public CreateVoucherDTO() { }
}

public class UpdateVoucherDTO
{
    public long Id { get; set; }
    public long WebshopId { get; set; }
    public decimal Amount { get; set; }
    public long UnitId { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? Code { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public long TypeId { get; set; }

    public UpdateVoucherDTO() { } 
}

public class VoucherProfile : Profile
{
    public VoucherProfile()
    {
        CreateMap<VoucherDTO, VoucherDb>().ReverseMap();
        CreateMap<CreateVoucherDTO, VoucherDb>().ReverseMap();
        CreateMap<UpdateVoucherDTO, VoucherDb>().ReverseMap();
    }
}


/*
public class VoucherDTO
{
    public long Id { get; set; }
    public long WebshopId { get; set; }
    public decimal Amount { get; set; }
    public long UnitId { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? Code { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public long TypeId { get; set; }
    public int Popularity { get { return (int)(Likes - Dislikes); } }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual WebshopDTO? Webshop { get; set; } = null;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual UnitDTO? Unit { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual VoucherTypeDTO? Type { get; set; } = null!;

    public VoucherDTO() { }

    public VoucherDTO(VoucherDb voucherDb,
                      bool includeWebshop = false,
                      bool includeUnit = false,
                      bool includeType = false)
    {
        Id = voucherDb.Id;
        Amount = voucherDb.Amount;
        Description = voucherDb.Description;
        Code = voucherDb.Code;
        ValidFrom = voucherDb.ValidFrom;
        ValidTo = voucherDb.ValidTo;
        Likes = voucherDb.Likes;
        Dislikes = voucherDb.Dislikes;

        TypeId = voucherDb.TypeId;
        UnitId = voucherDb.UnitId;
        WebshopId = voucherDb.WebshopId;

        Webshop = includeWebshop ? new WebshopDTO(voucherDb.Webshop) : null;
        Type = includeType ? new VoucherTypeDTO(voucherDb.Type) : null;
        Unit = includeUnit ? new UnitDTO(voucherDb.Unit): null;
    }

    //public static ValueTask<VoucherDTO?> BindAsync(HttpContext httpContext, System.Reflection.ParameterInfo parameter)
    //{
    //    int.TryParse(httpContext.Request.Query["id"], out var id);
    //    int.TryParse(httpContext.Request.Query["webshopId"], out var webshopId);
    //    int.TryParse(httpContext.Request.Query["amount"], out var amount);
    //    int.TryParse(httpContext.Request.Query["unitId"], out var unitId);

    //    var description = httpContext.Request.Query["description"];

    //    bool.TryParse(httpContext.Request.Query["isGift"], out var isGift);
    //    bool.TryParse(httpContext.Request.Query["isFreeDelivery"], out var isFreeDelivery);

    //    DateTime.TryParse(httpContext.Request.Query["validFrom"], out var validFrom);
    //    DateTime.TryParse(httpContext.Request.Query["validTo"], out var validTo);

    //    return ValueTask.FromResult<VoucherDTO?>(
    //        new VoucherDTO()
    //        {
    //            Id = id,
    //            WebshopId = webshopId,
    //            Amount = amount,
    //            UnitId = unitId,
    //            Description = description,
    //            IsGift = isGift,
    //            IsFreeDelivery = isFreeDelivery,
    //            ValidFrom = validFrom,
    //            ValidTo = validTo
    //        }
    //    );
    //}
}
*/