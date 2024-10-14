using System.Security.Cryptography;
using System.Text;
using Base.Domain;

namespace DAL.Domain;

public class Password : BaseEntity
{
    private User? _user;
    private string _userId;
    private byte[] _hash;

    public Password(User? user, string userId, string hash)
    {
        User = user;
        UserId = userId;
        Hash = hash;
    }

    public Password(User? user, string userId, byte[] hash)
    {
        User = user;
        UserId = userId;
        _hash = hash;
    }

    public User? User
    {
        get => _user;
        set => _user ??= value;
    }

    public string UserId
    {
        get => _userId;
        set
        {
            if (string.IsNullOrWhiteSpace(_userId)) _userId = value;
        }
    }

    public string Hash
    {
        get => _hash.ToString()!;
        set
        {
            if (value.Length != 512)
            {
                using (SHA512 hasher = new SHA512Managed())
                {
                    _hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
                }
            }
        }
    }
}