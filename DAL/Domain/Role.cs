using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDbGenericRepository.Attributes;

namespace DAL.Domain;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<string>
{
    
}