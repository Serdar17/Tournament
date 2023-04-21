using MediatR;
using Serilog;
using Tournament.Application.Interfaces;

namespace Tournament.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    private readonly ICurrentUserService _service;

    public LoggingBehavior(ICurrentUserService service)
    {
        _service = service;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _service.UserId;
        Log.Information("Competition request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        var response = await next();

        return response;
    }
}