using Base.Domain;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Domain;

public class Message : Base_Entity
{
    private User? _user;
    private string? _userId;
    private Chat? _chat;
    private string? _chatId;

    private bool _read;
    private bool _edited;
    private string? _text;
    
    //TODO: Add image support? or maybe video links support?
    
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    private DateTime? _publishedLocal;
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    private DateTime? _publishedUtc;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    private DateTime? _readedLocal;
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    private DateTime? _readedUtc;

    public Message(User? user, string userId, string text, string? chatId, Chat? chat)
    {
        User = user;
        UserId = userId;
        Text = text;
        ChatId = chatId;
        Chat = chat;
    }

    public Chat? Chat
    {
        get => _chat;
        set
        {
            if (_chat is null) _chat = value;
        }
    }
    
    public string? ChatId
    {
        get => _chatId;
        set
        {
            if (_chatId is null) _chatId = value;
        }
    }

    public string UserId
    {
        get => _userId!;
        set
        {
            if (!string.IsNullOrEmpty(_userId)) _userId = value;
        }
    }

    public User? User
    {
        get => _user;
        set
        {
            if (_user is null) _user = value;
        }
    }

    public bool Read
    {
        get => _read;
        set
        {
            if (!_read) _read = value;
        }
    }

    public bool Edited
    {
        get => _edited;
        set
        {
            if (!_edited) _edited = value;
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            _text = value;
        }
    }

    public void SetPublishingDateTime(DateTime local)
    {
        _publishedLocal = local;
        _publishedUtc = local.ToUniversalTime();
    }

    public void SetReadDateTime(DateTime local)
    {
        _readedLocal = local;
        _readedUtc = local.ToUniversalTime();
    }

    public DateTime? GetPublishingDateTime(bool isLocal)
    {
        if (isLocal) return _publishedLocal;
        else return _publishedUtc;
    }

    public DateTime? GetReadDateTime(bool isLocal)
    {
        if (isLocal) return _readedLocal;
        else return _readedUtc;
    }
}