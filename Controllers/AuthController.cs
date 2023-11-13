using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }


    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto request)
    {
        var existingUserByUsername = await _userManager.FindByNameAsync(request.UserName);
        var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);
        
        if (existingUserByUsername != null)
        {
            ModelState.AddModelError("UserName", "Username is already taken.");
        }

        if (existingUserByEmail != null)
        {
            ModelState.AddModelError("Email", "Email is already taken.");
        }
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserInfo = new UserInfo
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate,
                    Gender = request.Gender,
                    Genre = request.Genre,
                    ProfilePhotoUrl = request.ProfilePhotoUrl
                }
            };
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(user, new[] { "user" });
                if (identityResult.Succeeded)
                {
                    return Ok("User was registered! Please login");
                }
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkPassword)
            {
                //Get Roles 
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null)
                {
                    var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(response);
                }
            }
        }
        return BadRequest("Something went wrong");
    }
    
    [HttpPut]
    [Route("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile(string userId, [FromBody] UpdateProfileDto request)
    {
        // Get the current user
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var updatedUser = new User
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserInfo = new UserInfo
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate,
                    Gender = request.Gender,
                    Genre = request.Genre,
                    ProfilePhotoUrl = request.ProfilePhotoUrl
                }
            };
            // Update the user in the database
            var updateResult = await _userManager.UpdateAsync(updatedUser);

            if (updateResult.Succeeded)
            {
                return Ok("User profile updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update user profile.");
            }
        }

        return NotFound("User not found.");
    }

    [HttpDelete]
    [Route("DeleteAccount/{userId}")]
    public async Task<IActionResult> DeleteAccount(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok("Account deleted successfully.");
            }
            else
            {
                return BadRequest("Failed to delete account.");
            }
        }

        return NotFound("User not found.");
    }
    
}








