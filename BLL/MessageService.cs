using Base.Contracts.Repositories;
using Base.Contracts.Services;
using DAL.Domain;
using Microsoft.AspNet.Identity;

namespace BLL;

public class MessageService : BaseService<Message>, IMessageService<Message>
{
    public MessageService(IRepository<Message> repository) : base(repository)
    {
    }
    
    private IMessageRepository<Message> Repository => (IMessageRepository<Message>) _repository;

    public Task<List<Message>> GetMessagesByChatIdAsync(string chatId)
    {
        return Repository.GetMessagesByChatIdAsync(chatId);
    }

    public Task<List<Message>> GetMessagesByChatAndUserId(string chatId, string userId)
    {
        return Repository.GetMessagesByChatAndUserId(chatId, userId);
    }
}