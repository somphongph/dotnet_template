using System.Text.Json.Serialization;
using Domain.Enumerables;
using Domain.Extensions;

namespace Domain.Models;

public class ResponseCommand<T> : BaseResponse
{
    public ResponseCommand() { }
    public ResponseCommand(ResponseStatus status)
    {
        this.Code = status.Code();
        this.Message = status.NameString();
    }

    [JsonPropertyOrder(3)]
    public T? Data { get; set; }

}

