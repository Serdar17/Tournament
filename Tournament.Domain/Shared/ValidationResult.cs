namespace Tournament.Domain.Shared;

public class ValidationResult : IValidationResult
{
    private ValidationResult(Error[] errors, string title)
    {
        Errors = errors;
        Title = title;
    }

    private string Title { get; }
    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors, string title) => new(errors, title);
}