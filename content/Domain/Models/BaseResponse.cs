using System.Text.Json.Serialization;

namespace Domain.Models;

public abstract class BaseResponse
{
    [JsonPropertyOrder(1)]
    public string? Code { get; set; }

    [JsonPropertyOrder(2)]
    public string? Message { get; set; }
}
