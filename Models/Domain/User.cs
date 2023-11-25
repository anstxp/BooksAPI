using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BooksAPI.Models.DTO.AuthDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DeleteBehavior;

namespace BooksAPI.Models.Domain;

public class User : IdentityUser
{
    public UserInfo UserInfo { get; set; }
}