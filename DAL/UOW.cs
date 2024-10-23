using Base;
using DAL.Repositories;
using MongoDbGenericRepository;

namespace DAL;

public class Uow(IMongoDbContext ctx) : BaseUow<IMongoDbContext>(ctx)
{
    private ChatRepository? _chatRepository;
    public ChatRepository ChatRepository => _chatRepository ?? new ChatRepository(Ctx, "Chat");

    private MessageRepository? _messageRepository;
    public MessageRepository MessageRepository => _messageRepository ?? new MessageRepository(Ctx, "Message");
    

    private RefreshTokenRepository? _refreshTokenRepository;

    public RefreshTokenRepository RefreshTokenRepository =>
        _refreshTokenRepository ?? new RefreshTokenRepository(Ctx, "RefreshToken");
}