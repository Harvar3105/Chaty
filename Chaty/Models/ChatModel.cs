using System.ComponentModel.DataAnnotations;
using DAL.Domain;
using Helpers;
using Microsoft.AspNetCore.Components;

namespace Chaty.Models;

public class ChatModel
{
    [Length(minimumLength:3, maximumLength: 100, ErrorMessage = "Chat must have a name! Length must be between 3 and 100 characters!")]
    public string ChatName { get; set; } = default!;

    [MinLengthListAttr(2, ErrorMessage = "Chat must have at least 2 users exclude Admin!")]
    public List<User> Users = [];
}