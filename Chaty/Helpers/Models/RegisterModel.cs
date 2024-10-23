using System.ComponentModel.DataAnnotations;

namespace Chaty.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [Length(8, 30, ErrorMessage = "Password length must be between 8 and 30 characters")]
    public string Password { get; set; }

    public override string ToString()
    {
        return $"{Username} {Email} {FirstName} {LastName} {Age} {Password}";
    }
}