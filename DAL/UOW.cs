using Base;
using DAL.Repositories;
using MongoDbGenericRepository;

namespace DAL;

//TODO: should it be implemented? MongoDB usually does not support transactions
[Obsolete("This class is obsolete. Unclear should it be used or not.")]
public class Uow : BaseUow<IMongoDbContext>, IDisposable
{
    private readonly Lazy<ChatRepository> _chatRepository;
    public ChatRepository ChatRepository => _chatRepository.Value;

    private readonly Lazy<MessageRepository> _messageRepository;
    public MessageRepository MessageRepository => _messageRepository.Value;

    private readonly Lazy<RefreshTokenRepository> _refreshTokenRepository;
    public RefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository.Value;
    
    private readonly Lazy<FriendshipRepository> _friendshipRepository;
    public FriendshipRepository FriendshipRepository => _friendshipRepository.Value;

    public Uow(IMongoDbContext ctx) : base(ctx)
    {
        _friendshipRepository = new Lazy<FriendshipRepository>(() => new FriendshipRepository(Ctx, "Friendships"));
        _chatRepository = new Lazy<ChatRepository>(() => new ChatRepository(Ctx, "Chats"));
        _messageRepository = new Lazy<MessageRepository>(() => new MessageRepository(Ctx, "Messages"));
        _refreshTokenRepository = new Lazy<RefreshTokenRepository>(() => new RefreshTokenRepository(Ctx, "RefreshTokens"));
    }

    public void Dispose()
    {
        (Ctx as IDisposable)?.Dispose();
    }
}