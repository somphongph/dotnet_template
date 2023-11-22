using Domain.Models;
using MediatR;

namespace Domain.Handlers.Sample.GetSample
{
    public class GetSample : IRequest<ResponseItem<GetSampleResponse>>
    {

    }
}