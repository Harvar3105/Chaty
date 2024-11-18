using Base.Contracts.Entities;

namespace Base.Contracts.Services;

public interface IMessageService<T> : IService<T> where T : class, IEntity
{
    public Task<List<T>> GetMessagesByChatIdAsync(string chatId);
    public Task<List<T>> GetMessagesByChatAndUserId(string chatId, string userId);
}