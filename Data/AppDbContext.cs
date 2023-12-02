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
    public required DbSet<Collection> Collections { get; set; }
    public required DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public required DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public required DbSet<Order> Orders { get; set; }
    public required DbSet<OrderItem> OrderItems { get; set; }
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
        
        // Create an Admin User
        var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";
        var admin = new IdentityUser()
        {
            Id = adminUserId,
            UserName = "admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com".ToUpper(),
            NormalizedUserName = "admin@gmail.com".ToUpper()
        };

        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin$12345");

        builder.Entity<IdentityUser>().HasData(admin);

        // Give Roles To Admin

        var adminRoles = new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = adminUserId,
                RoleId = adminId
            },
        };

        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
}