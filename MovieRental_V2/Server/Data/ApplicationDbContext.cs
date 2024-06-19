using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using MovieRental_V2.Server.Models;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    
    
    public DbSet<Movie> Movies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        // You can configure your model here if needed
        modelBuilder.Entity<Movie>().HasKey(m => m.Id);
        
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Owner)
            .WithMany(u => u.Movies)
            .HasForeignKey(m => m.OwnerId);
    }
}

