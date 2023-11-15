using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.Domain;

public class User : IdentityUser
{
    [ForeignKey("UserId")]
    public UserInfo UserInfo { get; set; }
}