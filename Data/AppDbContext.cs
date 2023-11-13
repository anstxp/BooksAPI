using Microsoft.EntityFrameworkCore;
using BooksAPI.Models.Domain;

namespace BooksAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    public required DbSet<BookCategory> BookCategories { get; set; }
    public required DbSet<Book> Books { get; set; }
    public required DbSet<Author> Authors { get; set; }
    
    public required DbSet<BlogPost> BlogPosts { get; set; }
    public required DbSet<BlogPostCategory> BlogPostCategories { get; set; }
}