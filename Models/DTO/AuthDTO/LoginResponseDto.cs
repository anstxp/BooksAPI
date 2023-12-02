namespace BooksAPI.Models.DTO.AuthDto;

public class LoginResponseDto
{
    public string JwtToken { get; set; }
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}