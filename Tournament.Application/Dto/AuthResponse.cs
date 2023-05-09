namespace Tournament.Application.Dto;

public class AuthResponse
{
    public string? AccessToken { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public UserDto User { get; set; }
}