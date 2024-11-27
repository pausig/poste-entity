using EntityPoste.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityPoste;




public class AppDbContext : DbContext
{
    public AppDbContext() {}
    
    private static readonly ILoggerFactory myFactory = LoggerFactory.Create(b =>
    {
        b.AddConsole();
        b.SetMinimumLevel(LogLevel.Information);
    
    });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=COMPUTER\\SQLEXPRESS;Database=PosteDB;TrustServerCertificate=True;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(myFactory);
    }
    
    public DbSet<User> Users { get; set; }
}