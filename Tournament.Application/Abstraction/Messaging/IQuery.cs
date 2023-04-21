using Ardalis.Result;
using MediatR;

namespace Tournament.Application.Abstraction.Messaging;

public class IQuery<TResponse> : IRequest<Result<TResponse>>
{
}