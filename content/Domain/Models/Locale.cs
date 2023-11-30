using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Locale
{
    [BsonElement("en")]
    public string En { get; set; } = string.Empty;

    [BsonElement("th")]
    public string Th { get; set; } = string.Empty;

    [BsonElement("cn")]
    public string Cn { get; set; } = string.Empty;

    public override string ToString()
    {
        return Thread.CurrentThread.CurrentUICulture.Name.ToLower() switch
        {
            "en" or "en-us" => En,
            "th" or "th-th" => Th,
            "cn" or "zh-cn" => Cn,
            _ => En,
        };
    }

    public static Locale Create(string Th, string En, string Cn)
    {
        return new Locale()
        {
            En = En,
            Th = Th,
            Cn = Cn
        };
    }
}
