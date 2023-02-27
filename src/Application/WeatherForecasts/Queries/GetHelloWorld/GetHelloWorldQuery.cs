using MediatR;

namespace Application.WeatherForecasts.Queries.GetHelloWorld;

public record GetHelloWorldQuery : IRequest<string>;

public class GetHelloWorldQueryHandler : IRequestHandler<GetHelloWorldQuery, string>
{
    public Task<string> Handle(GetHelloWorldQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Hello World");
    }
}