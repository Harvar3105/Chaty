using Base;
using Base.Contracts.Entities;
using MongoDbGenericRepository.Attributes;

namespace DAL.Domain.AddressTables;

[CollectionName("Friendships")]
public class Friendship : BaseEntity
{
    public Friendship(string firstUserId, string secondUserId)
    {
        FirstUserId = firstUserId;
        SecondUserId = secondUserId;
    }

    public Friendship(User? firstUser, User? secondUser, string firstUserId, string secondUserId)
    {
        FirstUser = firstUser;
        SecondUser = secondUser;
        FirstUserId = firstUserId;
        SecondUserId = secondUserId;
    }

    public User? FirstUser { get; set; }
    public User? SecondUser { get; set; }

    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
}