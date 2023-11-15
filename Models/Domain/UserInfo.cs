
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.Domain;

public class UserInfo
{
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Genre { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
    public string ProfilePhotoUrl { get; set; }
    
}