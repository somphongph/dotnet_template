using Domain.Extensions;
using Domain.Models;

namespace Domain.Enumerables;

public enum RecordStatus
{
    Active,
    Inactive,
    Deleted
}

public static class RecordStatusName
{
    public static Locale Name(this RecordStatus enumValue) => Name(enumValue.Code());

    public static string NameString(this RecordStatus enumValue) => Name(enumValue.Code()).ToString() ?? "";

    public static Locale Name(string code) => Data().FirstOrDefault(m => m.Key.Equals(code)).Value ?? new Locale();

    public static Dictionary<string, Locale> Data()
    {
        var dict = new Dictionary<string, Locale>
            {
                { RecordStatus.Active.Code(), Locale.Create("ใช้งาน", "Active", "") },
                { RecordStatus.Inactive.Code(), Locale.Create("ไม่ใช้งาน", "Inactive", "") },
                { RecordStatus.Deleted.Code(), Locale.Create("ลบ", "Deleted", "") }
            };
        return dict;
    }
}
