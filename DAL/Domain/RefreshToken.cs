using Base;
using Base.Domain;

namespace DAL.Domain;

public class RefreshToken : BaseRefreshToken
{
    public string UserId { get; set; }
    public User? User { get; set; }
}