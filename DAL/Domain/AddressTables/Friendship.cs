using Base;
using Base.Contracts.Entities;
using MongoDbGenericRepository.Attributes;

namespace DAL.Domain.AddressTables;

[CollectionName("Friendships")]
public class Friendship(User firstUser, User secondUser, string firstUserId, string secondUserId)
    : BaseEntity
{
    public User? FirstUser { get; set; } = firstUser;
    public User? SecondUser { get; set; } = secondUser;

    public string FirstUserId { get; set; } = firstUserId;
    public string SecondUserId { get; set; } = secondUserId;
}