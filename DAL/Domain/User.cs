using System.Net.Mail;
using Base.Domain;
using Base.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Domain;

public class User : IdentityUser<string>, IEntity 
{
    public string Username { get; set; }
    private string? _firstName;
    private string? _lastName;
    private int _age;
    private MailAddress _email;
    public ICollection<RefreshToken> RefreshTokens;
    
    public User(){}
    
    public User(string username, string? firstName, string? lastName, string? email, int? age)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Age = age ?? 0;
        Email = email;
    }
    
    public string? FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
                
            if (value.All(Char.IsLetter)) _firstName = value;
            else throw new ArgumentException("Lastname can contain only letters! Provided: " + value);
        }
    }

    public string? LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
                
            if (value.All(Char.IsLetter)) _lastName = value;
            else throw new ArgumentException("Lastname can contain only letters! Provided: " + value);
        }
    }
    
    public int Age
    {
        get => _age;
        set
        {
            if (value <= 0) return;
            else _age = value;
        }
    }

    public string Email
    {
        get => _email.Address;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            try
            {
                _email = new MailAddress(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException("Provided unsupported data! Provided: " + value);
            }
        }
    }
}