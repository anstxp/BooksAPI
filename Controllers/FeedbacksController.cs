using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedbacksController : Controller
{
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbacksController(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFeedback(CreateFeedbackDto request)
    {
        //DTO to Domain model
        var feedback = new Feedback()
        {
            Name = request.Name,
            Phone = request.Phone,
            Date = DateTime.Now,
            Message = request.Message
        };

        await _feedbackRepository.CreateAsync(feedback);
        
        //Domain model to DTO
        var response = new FeedbackDto()
        {
            Id = feedback.Id,
            Name = feedback.Name,
            Date = feedback.Date,
            Phone = feedback.Phone,
            Message = feedback.Message
        };
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBookCategories()
    {
        var feedbacks = await _feedbackRepository.GetAllAsync();
        //Domain model to DTO
        var response = new List<FeedbackDto>();
        foreach (var feedback in feedbacks)
        {
            response.Add(new FeedbackDto
            {
                Id = feedback.Id,
                Name = feedback.Name,
                Phone = feedback.Phone,
                Date = feedback.Date,
                Message = feedback.Message,
            });
        }

        return Ok(response);
    }

    
    
}