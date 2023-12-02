using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.DTO.AuthDTO;

public class UserDto : IdentityUser
{
    [ForeignKey("UserInfoId")]
    public UserInfoDto UserInfo { get; set; }
}