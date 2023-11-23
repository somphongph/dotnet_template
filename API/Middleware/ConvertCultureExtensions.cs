namespace API.Middleware;

public class ConvertCultureExtensions
{
    public static string CultureCodeToStandardCultureCode(string letter)
    {
        letter = letter.Trim().ToLower();

        switch (letter)
        {
            case "en":
            case "en-us":
                return "en-US";
            case "th":
            case "th-th":
                return "th-TH";
            case "cn":
            case "zh-cn":
                return "zh-CN";
            default:
                return "en-US";
        }
    }
}
