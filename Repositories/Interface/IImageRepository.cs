using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.ImageDto;

namespace BooksAPI.Repositories.Interface;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
}