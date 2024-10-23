using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Chaty.Models;

public class LoginModel : IValidatableObject
{
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [Length(6, 30, ErrorMessage = "Password length must be between 8 and 30 characters")]
    public string Password { get; set; }

    public bool isEmail { get; private set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Login))
        {
            yield return new ValidationResult(
                "You must provide either a username or an email address.",
                new[] { nameof(Login)});
        }
        else
        {
            try
            {
                var mail = new MailAddress(Login);
                isEmail = true;
            }
            catch (FormatException e)
            {
                isEmail = false;
            }
        }

    }

    public override string ToString()
    {
        return $"{Login} {Password}";
    }
}