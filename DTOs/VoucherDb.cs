using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using VouchersBackend.Helpers;

namespace VouchersBackend.Models;

public class VoucherDb
{
    [Key]
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? Code { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }

    public long TypeId { get; set; }
    public virtual VoucherTypeDb Type { get; set; } = null!;

    public long UnitId { get; set; }
    public virtual UnitDb Unit { get; set; } = null! ;

    public long WebshopId { get; set; }
    public virtual WebshopDb Webshop { get; set; } = null! ;

    public VoucherDb() { }

    public VoucherDb(VoucherDTO voucherDTO)
    {
        WebshopId = voucherDTO.WebshopId;
        UnitId = voucherDTO.UnitId;
        TypeId = voucherDTO.TypeId;

        Amount = voucherDTO.Amount;
        Description = voucherDTO.Description;
        Code = voucherDTO.Code;

        Likes = voucherDTO.Likes;
        Dislikes = voucherDTO.Dislikes;

        ValidFrom = voucherDTO.ValidFrom;
        ValidTo = voucherDTO.ValidTo;
    }
}

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