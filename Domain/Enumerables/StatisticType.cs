using Domain.Extensions;
using Domain.Models;

namespace Domain.Enumerables;

public enum StatisticType
{
    ShowOnList,
    ShowOnDetail
}

public static class StatisticTypeName
{
    public static Locale Name(this StatisticType enumValue) => Name(enumValue.Code());

    public static string NameString(this StatisticType enumValue) => Name(enumValue.Code()).ToString() ?? "";

    public static Locale Name(string code) => Data().FirstOrDefault(m => m.Key.Equals(code)).Value ?? new Locale();

    public static Dictionary<string, Locale> Data()
    {
        var dict = new Dictionary<string, Locale>
            {
                { StatisticType.ShowOnList.Code(), Locale.Create("แสดง List", "ShowOnList", "") },
                { StatisticType.ShowOnDetail.Code(), Locale.Create("แสดง Detail", "ShowOnDetail", "") }
            };
        return dict;
    }
}
