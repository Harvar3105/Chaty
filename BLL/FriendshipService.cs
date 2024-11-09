using Base.Contracts.Repositories;
using Base.Contracts.Services;
using DAL.Domain.AddressTables;

namespace BLL;

public class FriendshipService : BaseService<Friendship>, IFriendshipService<Friendship>
{
    public FriendshipService(IRepository<Friendship> repository) : base(repository)
    {
    }
}