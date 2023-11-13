
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Models.Domain;

public class UserInfo
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Genre { get; set; }
    public DateTime Birthdate { get; set; }
    public string ProfilePhotoUrl { get; set; }
    
}