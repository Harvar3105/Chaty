namespace Helpers;

public class JWT
{
    public string? UserId { get; set; } = default!;
    public string Jwt { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}