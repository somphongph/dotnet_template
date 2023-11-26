using Domain.Extensions;
using Domain.Models;

namespace Domain.Enumerables;

public enum RecordStatus
{
    Active,
    Disabled,
    Deleted
}

public static class RecordStatusName
{
    public static Locale Name(this RecordStatus enumValue) => NameCode(enumValue.Code());

    public static string NameString(this RecordStatus enumValue) => NameCode(enumValue.Code()).ToString() ?? "";

    public static Locale NameCode(string code) => Data().FirstOrDefault(m => m.Key.Equals(code)).Value ?? new Locale();

    public static Dictionary<string, Locale> Data()
    {
        var dict = new Dictionary<string, Locale>
            {
                { RecordStatus.Active.Code(), Locale.Create("ใช้งาน", "Active", "") },
                { RecordStatus.Disabled.Code(), Locale.Create("ไม่ใช้งาน", "Disabled", "") },
                { RecordStatus.Deleted.Code(), Locale.Create("ลบ", "Deleted", "") }
            };
        return dict;
    }
}
