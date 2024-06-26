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
    
    
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<GenreModel> Genres { get; set; }
    public DbSet<MovieGenreModel> MovieGenres { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        // You can configure your model here if needed
        modelBuilder.Entity<MovieModel>().HasKey(m => m.Id);
        
        modelBuilder.Entity<MovieModel>()
            .HasOne(m => m.Owner)
            .WithMany(u => u.Movies)
            .HasForeignKey(m => m.OwnerId);
        
        modelBuilder.Entity<MovieGenreModel>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieGenreModel>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.MovieGenres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenreModel>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId);
    }
}

