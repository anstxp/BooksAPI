using BooksAPI.Data;
using BooksAPI.Models.Domain;
using BooksAPI.Repositories.Interface;

namespace BooksAPI.Repositories.Implementation;

public class ImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _appDbContext;

    public ImageRepository(IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor,
        AppDbContext appDbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _appDbContext = appDbContext;
    }
    
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", 
            $"{image.Name}{image.FileExtension}");

        using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.Name}{image.FileExtension}";

        image.FilePath = urlFilePath;
        
        //Add to Images
        await _appDbContext.Images.AddAsync(image);
        await _appDbContext.SaveChangesAsync();

        return image;
    }
}