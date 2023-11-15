using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO.AuthDto;

public class UpdateProfileDto
{
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    [MaxLength(10, ErrorMessage = "Username has to be maximum of 10 characters")]
    public string UserName { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    [DataType(DataType.Date)]
    public DateTime Birthdate { get; set; }
    public string Gender { get; set; }
    public string Genre { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ProfilePhotoUrl { get; set; }
}