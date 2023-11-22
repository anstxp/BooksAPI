using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.AuthDTO;

public class UserInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Genre { get; set; }
    public DateTime Birthdate { get; set; }
    public string ProfilePhotoUrl { get; set; }
}