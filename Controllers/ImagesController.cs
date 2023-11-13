using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.ImageDto;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : Controller
{
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromForm] UploadImageDto request)
    {
        ValidateFileUpload(request);
        
        if (ModelState.IsValid)
        {
             //DTO to Domain Model
             var imageDomainModel = new Image
             {
                 File = request.File,
                 FileExtension = Path.GetExtension(request.File.FileName),
                 FileSizeInBytes = request.File.Length,
                 Name = request.Name,
                 Descroption = request.FileDescription
             };

             await _imageRepository.Upload(imageDomainModel);
             return Ok(imageDomainModel);
        }

        return BadRequest(ModelState);
    }

    private void ValidateFileUpload(UploadImageDto request)
    {
        var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
        if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
        {
            ModelState.AddModelError("file", "Unsupported file extension");
        }

        if (request.File.Length > 10485760)
        {
            ModelState.AddModelError("file", 
                "File size more than 10MB, please upload a smaller size file");

        }
    }
}