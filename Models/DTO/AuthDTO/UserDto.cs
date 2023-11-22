using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.DTO.AuthDTO;

public class UserDto 
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    [ForeignKey("UserInfoId")]
    public UserInfoDto UserInfo { get; set; }
}