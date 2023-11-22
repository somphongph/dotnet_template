using Domain.Enumerables;
using Domain.Models;
using MediatR;

namespace Domain.Handlers.Sample.GetSample
{
    public class GetSampleHandler : IRequestHandler<GetSample, ResponseItem<GetSampleResponse>>
    {
        public GetSampleHandler()
        {
        }

        public Task<ResponseItem<GetSampleResponse>> Handle(GetSample req, CancellationToken cancellationToken)
        {
            var res = new GetSampleResponse()
            {
                Name = "asdfasdf"
            };

            return Task.FromResult(new ResponseItem<GetSampleResponse>(ResponseStatus.Success)
            {
                Data = res,
                IsCached = false,
            });

        }

    }
}