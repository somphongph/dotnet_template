using Domain.Extensions;
using Domain.Models;

namespace Domain.Enumerables;

public enum ResponseStatus
{
    Success,
    Failed,
    DataNotFound,
    DataInvalid,
    DataDuplicated
}

public static class ResponseCodeName
{
    public static Locale Name(this ResponseStatus enumValue) => NameCode(enumValue.Code());

    public static string NameString(this ResponseStatus enumValue) => NameCode(enumValue.Code()).ToString() ?? "";

    public static Locale NameCode(string code) => Data().FirstOrDefault(m => m.Key.Equals(code)).Value ?? new Locale();

    public static Dictionary<string, Locale> Data()
    {
        var dict = new Dictionary<string, Locale>
        {
            { ResponseStatus.Success.Code(), Locale.Create("การดำเนินการสำเร็จ", "Action successfully", "") },
            { ResponseStatus.Failed.Code(), Locale.Create("การดำเนินการไม่สำเร็จ", "Action failed", "") },
            { ResponseStatus.DataNotFound.Code(), Locale.Create("ไม่พบข้อมูล", "Data not found", "") },
            { ResponseStatus.DataInvalid.Code(), Locale.Create("ข้อมูลไม่ถูกต้อง", "Data invalid", "") },
            { ResponseStatus.DataDuplicated.Code(), Locale.Create("ไม่สามารถระบุข้อมูลซ้ำกันได้", "Data duplicated", "") }
        };
        return dict;
    }
}
