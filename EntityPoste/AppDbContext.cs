using EntityPoste.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityPoste;

public class AppDbContext : DbContext
{
    private static readonly ILoggerFactory MyFactory = LoggerFactory.Create(b =>
    {
        b.AddConsole();
        b.SetMinimumLevel(LogLevel.Information);
    });

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=PosteDB;TrustServerCertificate=True;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(MyFactory);
    }
}