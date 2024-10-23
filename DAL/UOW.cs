using Base;
using DAL.Repositories;
using MongoDbGenericRepository;

namespace DAL;

//TODO: is it really disposable?
public class Uow : BaseUow<IMongoDbContext>, IDisposable
{
    private readonly Lazy<ChatRepository> _chatRepository;
    public ChatRepository ChatRepository => _chatRepository.Value;

    private readonly Lazy<MessageRepository> _messageRepository;
    public MessageRepository MessageRepository => _messageRepository.Value;

    private readonly Lazy<RefreshTokenRepository> _refreshTokenRepository;
    public RefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;

    public Uow(IMongoDbContext ctx) : base(ctx)
    {
        _chatRepository = new Lazy<ChatRepository>(() => new ChatRepository(Ctx, "Chat"));
        _messageRepository = new Lazy<MessageRepository>(() => new MessageRepository(Ctx, "Message"));
        _refreshTokenRepository = new Lazy<RefreshTokenRepository>(() => new RefreshTokenRepository(Ctx, "RefreshToken"));
    }

    public void Dispose()
    {
        (Ctx as IDisposable)?.Dispose();
    }
}