using Base.Domain;

namespace DAL.Domain;

public class Chat : Base_Entity
{
    public List<string>? UsersIds = new List<string>();
    public List<User>? Users = new List<User>();
    private User? _admin;
    private string? _adminId;
    private string _chatName;
    
    //TODO: Chat icon?

    public Chat(string chatName, User? admin, string adminId)
    {
        ChatName = chatName;
        Admin = admin;
        AdminId = adminId;
    }

    public string? AdminId
    {
        get => _adminId;
        set
        {
            if (!string.IsNullOrWhiteSpace(value)) _adminId = value;
        }
    }

    public User? Admin
    {
        get => _admin;
        set => _admin ??= value;
    }

    public string ChatName
    {
        get => _chatName;
        set
        {
            if (!string.IsNullOrWhiteSpace(value)) _chatName = value;
            else throw new ArgumentNullException(nameof(value), "Chat name cannot be empty!");
        }
    }
}