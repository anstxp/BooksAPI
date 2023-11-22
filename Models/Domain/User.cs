using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DeleteBehavior;

namespace BooksAPI.Models.Domain;

public class User : IdentityUser
{
    [ForeignKey("UserInfoId")]
    public UserInfo UserInfo { get; set; }
}