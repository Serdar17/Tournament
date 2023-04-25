using Ardalis.Result;

namespace Tournament.Domain.Shared;

public class ValidationResult<TValue> : Result, IValidationResult
{
    private ValidationResult(Error[] errors) =>
        Errors = errors;
        
    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
}