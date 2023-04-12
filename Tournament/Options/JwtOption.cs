namespace Tournament.Options;

public class JwtOption
{
    public const string Section = "JwtOption";

    public string Key { get; set; } = String.Empty;

    public string Issuer { get; set; } = String.Empty;
    
    public string Audience { get; set; } = String.Empty;

    public int AccessTokenExpiryDurationMinutes { get; set; }
    
    public int RefreshTokenExpiryDurationDays { get; set; }
}