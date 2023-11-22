using System.Globalization;

namespace Domain.Extensions;

public static class StringExtension
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat((str)
                .Select((x, i) => i > 0 && i < str.Length - 1 && char.IsUpper(x) && !char.IsUpper(str[i - 1])
                    ? $"_{x}"
                    : x.ToString())).ToLower();
    }

    public static string ToPascalCase(this string str)
    {
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        var arr = str
            .ToLower()
            .Split('_')
            .Select(ti.ToTitleCase);

        return string.Join("", arr);
    }

    public static bool HasValue(this string? str)
    {
        return !string.IsNullOrEmpty(str);
    }
}
