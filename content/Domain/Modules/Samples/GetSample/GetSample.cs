using Domain.Models;
using MediatR;

namespace Domain.Modules.Samples.GetSample;

public class GetSample : IRequest<ResponseItem<GetSampleResponse>>
{

}
