using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;
using BooksAPI.Models.Domain;

namespace BooksAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options):base(options)
    {
    }
    public required DbSet<BookCategory> BookCategories { get; set; }
    public required DbSet<Book> Books { get; set; }
    public required DbSet<Author> Authors { get; set; }
    
}