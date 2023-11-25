using Microsoft.EntityFrameworkCore;
using BooksAPI.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BooksAPI.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    public required DbSet<BookCategory> BookCategories { get; set; }
    public required DbSet<Book> Books { get; set; }
    public required DbSet<Author> Authors { get; set; }
    
    public required DbSet<BlogPost> BlogPosts { get; set; }
    public required DbSet<Image> Images { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<UserInfo> UserInfos { get; set; }
    
    public required DbSet<Comment> Comments { get; set; }
    public required DbSet<Feedback> Feedbacks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var userId = "4DA819B8-8CF0-4469-913F-D468B2D8E8CC";
        var adminId = "938976C5-B4D3-4537-9B70-D97406608370";
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = userId,
                ConcurrencyStamp = userId,
                Name = "user",
                NormalizedName = "user".ToUpper()
            },
            new IdentityRole
            {
                Id = adminId,
                ConcurrencyStamp = adminId,
                Name = "admin",
                NormalizedName = "admin".ToUpper()
            }
        };
        
        builder.Entity<IdentityRole>().HasData(roles);
    }
}