using System.ComponentModel.DataAnnotations;

namespace Chaty.Models;

public class UserSearchModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    public string Username { get; set; } = string.Empty;
}