using Base.Contracts.Repositories;
using Base.Contracts.Services;
using DAL.Domain;

namespace BLL;

public class ChatService : BaseService<Chat>, IChatService<Chat>
{
    public ChatService(IRepository<Chat> repository) : base(repository)
    {
    }
    
    private IChatRepository<Chat> Repository => (IChatRepository<Chat>) _repository;

    public async Task<List<Chat>?> GetByUserIdAsync(string userId)
    {
        return await Repository.GetByUserIdAsync(userId);
    }
}