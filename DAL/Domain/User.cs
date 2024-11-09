using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace DAL.Domain
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<string>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [StringLength(30)]
        // public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; } = 0;
        // public string Email { get; set; }
        [BsonIgnore]
        public ICollection<RefreshToken?> RefreshTokens { get; set; } = new List<RefreshToken?>();

        public User()
        {
        }

        public User(string username, string? firstName, string? lastName, string email, int? age = 0)
        {
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
            Age = age ?? 0;
            Email = email;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Username: {UserName}, FirstName: {FirstName}, LastName: {LastName}, Age: {Age}, RefreshRoken: {RefreshTokens}";
        }
    }
}