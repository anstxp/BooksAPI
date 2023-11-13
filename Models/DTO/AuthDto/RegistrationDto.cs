using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO;

public class RegistrationDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    [MaxLength(10, ErrorMessage = "Username has to be maximum of 10 characters")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [MinLength(3, ErrorMessage = "Username has to be minimum of 3 characters")]
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthdate { get; set; }
    [Required]
    public string Gender { get; set; }
    public string Genre { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string ProfilePhotoUrl { get; set; }
}