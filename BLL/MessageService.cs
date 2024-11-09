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
    
}