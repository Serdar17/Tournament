using Ardalis.Result;
using FluentValidation;
using MediatR;
using Tournament.Domain.Shared;

namespace Tournament.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(failure => failure is not null)
            .ToList();
        
        if (failures.Any())
        {
            // return ValidationResult.WithErrors(errors);
            throw new ValidationException(failures);
            // var a = CreateValidationResult<TResponse>(errors);
            // return a;
        }

        return await next();
    }

    // private static TResult CreateValidationResult<TResult>(Error[] errors)
    //     where TResult : Result
    // {
    //     if (typeof(TResult) == typeof(Result))
    //     {
    //         return (ValidationResult.WithErrors(errors) as TResult)!;
    //     }
    //
    //     object validationResult = typeof(ValidationResult<>)
    //         .GetGenericTypeDefinition()
    //         .MakeGenericType(typeof(Result).GenericTypeArguments[0])
    //         .GetMethod(nameof(ValidationResult.WithErrors))
    //         .Invoke(null, new object?[] { errors });
    //
    //     return (TResult)validationResult;
    // }
}