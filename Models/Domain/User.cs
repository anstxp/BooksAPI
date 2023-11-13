using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.Domain;

public class User : IdentityUser
{
    public UserInfo? UserInfo { get; set; }
}