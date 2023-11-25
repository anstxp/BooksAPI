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
                //Get Roles 
                var roles = await _userManager.GetRolesAsync(user);
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
    
    // [Authorize]
    // [HttpPut("EditUser")]
    // public async Task<IActionResult> PatchUser(UpdateProfileDto userModel)
    // {
    //     User user = (await _userManager.GetUserAsync(User) as User);
    //     if (user != null)
    //     { 
    //         user.UpdateFromUserModel(userModel); // update all fields except password
    //             
    //         await _userManager.UpdateAsync(user);
    //         return Ok();
    //     }
    //     return BadRequest();
    // }

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
    
    [HttpGet("GetRole")]
    public async Task<IActionResult> GetUserRole()
    {
        var claimsPrincipal = User;
        if (claimsPrincipal != null)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            if (identityUser != null)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);
                var role = roles.FirstOrDefault();
                return Ok(role);
            }
        }
        return NotFound();
    }
    
    [HttpPatch]
    [Route("{id:Guid}")]
    public async Task<IActionResult> EditUser([FromRoute] Guid id, [FromBody]UpdateProfileDto request)
    {
        var existedUser = await _userRepository.GetById(id);
        
        var user = new User
        {
            Id = id.ToString(),
            UserName = request.UserName,
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
        
        var updatedUser = await _userManager.UpdateAsync(user);
        // Convert Domain model to DTO
        var response = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            UserInfo = new UserInfoDto()
            {
                FirstName = user.UserInfo.FirstName,
                LastName = user.UserInfo.LastName,
                Birthdate = user.UserInfo.Birthdate,
                Gender = user.UserInfo.Gender,
                Genre = user.UserInfo.Genre,
                ProfilePhotoUrl = user.UserInfo.ProfilePhotoUrl
            }
        };
        return Ok(response);
    }
}








