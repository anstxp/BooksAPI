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
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Birthdate = request.Birthdate,
                Gender = request.Gender,
                Genre = request.Genre,
                ProfilePhotoUrl = request.ProfilePhotoUrl
            };

            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                //Add roles to this user
                if (request.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(user, request.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
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
    [Route("{id:Guid}")]
    public async Task<IActionResult> ChangeUserProfile([FromRoute] Guid id, UpdateProfileDto request)
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
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            //DTO to Domain Model
            var updatedUser = new User
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Birthdate = request.Birthdate,
                Gender = request.Gender,
                Genre = request.Genre,
                ProfilePhotoUrl = request.ProfilePhotoUrl
            };
            await _userManager.UpdateAsync(updatedUser);
            
            //Domain model to DTO
            var response = new UserDto
            {
                UserName = updatedUser.UserName,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                PhoneNumber = updatedUser.PhoneNumber,
                Email = updatedUser.Email,
                Birthdate = updatedUser.Birthdate,
                Gender = updatedUser.Gender,
                Genre = updatedUser.Genre,
                ProfilePhotoUrl = updatedUser.ProfilePhotoUrl
            };
            return Ok(response);
        }
        return BadRequest("Invalid password or user not found");
    }
    
}








