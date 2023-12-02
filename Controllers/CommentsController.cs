using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.AuthorDto;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Models.DTO.CommentDTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    
    public CommentsController(IBookRepository bookRepository, 
        ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _bookRepository = bookRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto request)
    {
        var existingUser = await _userRepository.GetById(request.User);
        var existingBook = await _bookRepository.GetById(request.Book);

        if (existingUser == null || existingBook == null) return NotFound();
        
        var comment = new Comment
        {
            Content = request.Content,
            Date = DateTime.UtcNow,
            User = existingUser,
            Book = existingBook
        };

        comment = await _commentRepository.CreateAsync(comment);
         var response = new CommentDto
         {
             Id = comment.Id,
             Content = comment.Content,
             PublishDate = request.Date,
             User = new UserDto()
             {
                 Id = comment.User.Id,
                 UserName = comment.User.UserName!,
                 Email = comment.User.Email!,
                 UserInfo = new UserInfoDto()
                 {
                     FirstName = comment.User.UserInfo.FirstName,
                     LastName = comment.User.UserInfo.LastName,
                     ProfilePhotoUrl = comment.User.UserInfo.ProfilePhotoUrl
                 }
             },
             Book = new BookDto
             {
                 Id = comment.Id,
                 Title = comment.Book.Title,
                 Description = comment.Book.Description,
                 UrlHadle = comment.Book.UrlHandle,
                 ImageUrl = comment.Book.ImageUrl,
                 Categories = comment.Book.Categories.Select(category => new BookCategoryDto
                 {
                     Id = category.Id,
                     Name = category.Name,
                     Description = category.Description!,
                     UrlHandle = category.UrlHandle,
                 }).ToList(),
                 Authors = comment.Book.Authors.Select(author => new AuthorDto
                 {
                     Id = author.Id,
                     FullName = author.FullName,
                     Description = author.Description!,
                     UrlHandle = author.UrlHandle
                 }).ToList()
             },
         };
         return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
        var comments = await _commentRepository.GetCommentsAsync();
        var response = new List<CommentDto>();
        foreach(var comment in comments)
        {
            response.Add(new CommentDto
            {
                Id = comment!.Id,
                Content = comment.Content,
                PublishDate = comment.Date,
                User = new UserDto()
                {
                    Id = comment.User.Id,
                    UserName = comment.User.UserName!,
                    Email = comment.User.Email!,
                    UserInfo = new UserInfoDto()
                    {
                        FirstName = comment.User.UserInfo.FirstName,
                        LastName = comment.User.UserInfo.LastName,
                        ProfilePhotoUrl = comment.User.UserInfo.ProfilePhotoUrl
                    }
                },
                Book = new BookDto
                {
                    Id = comment.Id,
                    Title = comment.Book.Title,
                    Description = comment.Book.Description,
                    UrlHadle = comment.Book.UrlHandle,
                    ImageUrl = comment.Book.ImageUrl,
                    Authors = comment.Book.Authors.Select(author => new AuthorDto
                    {
                        Id = author.Id,
                        FullName = author.FullName,
                        Description = author.Description!,
                        UrlHandle = author.UrlHandle
                    }).ToList()                
                },
            });
        }
        return Ok(response);
    }
}