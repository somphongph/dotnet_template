using System.Text.Json.Serialization;
using Domain.Enumerables;
using Domain.Extensions;

namespace Domain.Models;

public class ResponseItem<T> : BaseResponse
{
    public ResponseItem() { }
    public ResponseItem(ResponseStatus status)
    {
        this.Code = status.Code();
        this.Message = status.NameString();
    }

    [JsonPropertyOrder(4)]
    public bool IsCached { get; set; }

    [JsonPropertyOrder(5)]
    public T? Data { get; set; }
}
