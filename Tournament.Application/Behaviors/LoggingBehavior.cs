using MediatR;
using Microsoft.Extensions.Logging;
using Tournament.Application.Interfaces;

namespace Tournament.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    private readonly ICurrentUserService _service;
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ICurrentUserService service, ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _service.UserId;
        _logger.LogInformation("Entity from request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        var response = await next();
    
        _logger.LogInformation("Handled {Name} object {@Response}", typeof(TResponse).Name, typeof(TResponse));
                
        return response;
    }
}