using Base.Contracts.Entities;

namespace Base.Contracts.Repositories;

public interface IMessageRepository<T> : IRepository<T> where T : class, IEntity
{
    public Task<List<T>> GetMessagesByChatIdAsync(string chatId);
    public Task<List<T>> GetMessagesByChatAndUserId(string chatId, string userId);
}