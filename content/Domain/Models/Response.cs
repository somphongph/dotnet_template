using System.Text.Json.Serialization;
using Domain.Enumerables;
using Domain.Extensions;

namespace Domain.Models;

public class Response<T> : BaseResponse
{
    public Response() { }
    public Response(ResponseStatus status)
    {
        Code = status.Code();
        Message = status.NameString();
    }

    [JsonPropertyOrder(3)]
    public T? Data { get; set; }

}

