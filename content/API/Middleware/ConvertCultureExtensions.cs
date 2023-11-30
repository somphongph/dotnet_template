namespace API.Middleware;

public class ConvertCultureExtensions
{
    public static string CultureCodeToStandardCultureCode(string letter)
    {
        letter = letter.Trim().ToLower();

        return letter switch
        {
            "en" or "en-us" => "en-US",
            "th" or "th-th" => "th-TH",
            "cn" or "zh-cn" => "zh-CN",
            _ => "en-US",
        };
    }
}
