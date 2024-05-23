using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemAuthor> ItemAuthors { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<LibraryMember> LibraryMembers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
