using BooksAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.Repositories.Interface;

public interface ITokenRepository
{ 
    string CreateJwtToken(IdentityUser user, List<string> roles);
}