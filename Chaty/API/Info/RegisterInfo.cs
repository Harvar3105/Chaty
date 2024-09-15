namespace Chaty.API.Info;

public class RegisterInfo
{
    public string Username { get; set; } = default!;
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public string? Email { get; set; } = default!;
}