namespace Tournament.Domain.Shared;

public interface IValidationResult
{
    public static Error ValidationError = new("Validation Error", "A validation problem occurred");

    Error[] Errors { get; }
}