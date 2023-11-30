using System.Text.Json.Serialization;
using Domain.Enumerables;
using Domain.Extensions;

namespace Domain.Models;

public class ResponseList<T> : BaseResponse
{
    public ResponseList() { }
    public ResponseList(ResponseStatus status)
    {
        Code = status.Code();
        Message = status.NameString();
    }

    [JsonPropertyOrder(4)]
    public bool IsCached { get; set; }

    [JsonPropertyOrder(5)]
    public IEnumerable<T>? Data { get; set; }
}
