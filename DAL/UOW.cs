using Base;
using DAL.Repositories;
using MongoDB.Driver;

namespace DAL;

public class UOW : BaseUOW<IMongoDatabase>
{
    public UOW(IMongoDatabase db) : base(db)
    {
    }

    private ChatRepository? _chatRepository;
    public ChatRepository ChatRepository => _chatRepository ?? new ChatRepository(_db, "Chat");

    private MessageRepository? _messageRepository;
    public MessageRepository MessageRepository => _messageRepository ?? new MessageRepository(_db, "Message");

    private PasswordRepository? _passwordRepository;
    public PasswordRepository PasswordRepository => _passwordRepository ?? new PasswordRepository(_db, "Password");

    private RefreshTokenRepository? _refreshTokenRepository;

    public RefreshTokenRepository RefreshTokenRepository =>
        _refreshTokenRepository ?? new RefreshTokenRepository(_db, "RefreshToken");
}