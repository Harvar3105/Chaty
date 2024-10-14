using System.Net.Mail;

namespace Chaty.Helpers.Models;

public class LoginModel
{
    public String? Username { get; set; }
    public String? Email { get; set; }
    public required String Password { get; set; }
}