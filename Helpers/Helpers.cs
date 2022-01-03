//using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VouchersBackend.Helpers;

// TODO Get these enums out of here
[JsonConverter(typeof(StringEnumConverter))]
public enum SortingParameter
{
    Likes,
    Dislikes,
    DateAdded,
    Amount,
    Popularity
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SortingDirection
{
    Ascending,
    Descending
}

public static class SortingDirectionEnumExtension
{
    public static string ToDbQueryString(this SortingDirection me)
    {
        switch (me)
        {
            case SortingDirection.Ascending:
                return "ASC";
            case SortingDirection.Descending:
                return "DESC";
            default:
                return "Get your damn dirty hands off me you FILTHY APE!";
        }
    }

    public static string ToDbQueryString(this SortingParameter me)
    {
        switch (me)
        {
            case SortingParameter.Likes:
                return "Likes";
            case SortingParameter.Dislikes:
                return "Dislikes";
            case SortingParameter.Amount:
                return "Amount";
            case SortingParameter.DateAdded:
                return "ValidFrom";
            case SortingParameter.Popularity:
                return "(Likes - Dislikes)";
            default:
                return "Get your damn dirty hands off me you FILTHY APE!";
        }
    }
}