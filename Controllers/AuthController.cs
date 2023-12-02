using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.AuthDto;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;

    public AuthController(UserManager<IdentityUser> userManager, 
        ITokenRepository tokenRepository,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
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
                var roles = await _userManager.GetRolesAsync(user);
                {
                    var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken,
                        UserId = user.Id,
                        Roles = roles.ToList() // Передайте список ролей користувача
                    };
                    return Ok(response);
                }
            }
        }
        return BadRequest("Something went wrong");
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetInfo([FromRoute] Guid id)
    {
        var existingUser = await _userRepository.GetById(id);
        if (existingUser is null)
            return NotFound();
        var user = new UserDto()
        {
            UserName = existingUser.UserName!,
            PhoneNumber = existingUser.PhoneNumber,
            Email = existingUser.Email!,
            UserInfo = new UserInfoDto()
            {
                FirstName = existingUser.UserInfo.FirstName,
                LastName = existingUser.UserInfo.LastName,
                ProfilePhotoUrl = existingUser.UserInfo.ProfilePhotoUrl,
                Gender = existingUser.UserInfo.Gender,
                Genre = existingUser.UserInfo.Genre,
                Birthdate = existingUser.UserInfo.Birthdate
            }
        };
        return Ok(user);
    }

    [HttpDelete]
    [Route("DeleteAccount/{userId}")]
    public async Task<IActionResult> DeleteAccount(Guid userId)
    {
        var result = await _userRepository.DeleteAsync(userId);

        if (result != null)
        {
            return Ok("Account deleted successfully.");
        }
        else
        {
            return BadRequest("Failed to delete account.");
        }
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditUser([FromRoute] Guid id, [FromBody] UpdateProfileDto request)
    {
        var existedUser = await _userRepository.GetById(id);
        if (existedUser == null) NotFound();

        // Modify the existing user entity with the updated values
        existedUser.UserName = request.UserName;
        existedUser.Email = request.Email;
        existedUser.UserInfo.FirstName = request.FirstName;
        existedUser.UserInfo.LastName = request.LastName;
        existedUser.UserInfo.Birthdate = request.Birthdate;
        existedUser.UserInfo.Gender = request.Gender;
        existedUser.UserInfo.Genre = request.Genre;
        existedUser.UserInfo.ProfilePhotoUrl = request.ProfilePhotoUrl;

        // Save changes to the database
        await _userRepository.UpdateAsync(existedUser);

        var response = new UserDto
        {
            Id = existedUser.Id,
            UserName = existedUser.UserName,
            Email = existedUser.Email,
            UserInfo = new UserInfoDto()
            {
                FirstName = existedUser.UserInfo.FirstName,
                LastName = existedUser.UserInfo.LastName,
                Birthdate = existedUser.UserInfo.Birthdate,
                Gender = existedUser.UserInfo.Gender,
                Genre = existedUser.UserInfo.Genre,
                ProfilePhotoUrl = existedUser.UserInfo.ProfilePhotoUrl
            }
        };

        return Ok(response);
    }
}








