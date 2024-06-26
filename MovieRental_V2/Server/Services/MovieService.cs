using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental_V2.Server.Data;
using MovieRental_V2.Shared.Dtos;

namespace MovieRental_V2.Server.Services;

public class MovieService
{
    
    private readonly ApplicationDbContext _context;
    
    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<MovieDto>> GetMovies()
    {
        List<MovieDto> movies = await _context.Movies
            .Include(m => m.Owner)
            .Include(m => m.MovieGenres)
            .ThenInclude(mg => mg.Genre)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Available = m.Available,
                Director = m.Director,
                ReleaseDate = m.ReleaseDate,
                OwnerName = m.Owner!.UserName,
                Genres = m.MovieGenres.Select(mg => mg.Genre).ToList()
            })
            .ToListAsync();

        return movies;  
    }

    public async Task<MovieDto?> GetMovie()
    {
        MovieDto? movie = await _context.Movies
            .Include(m => m.Owner)
            .Include(m => m.MovieGenres)
            .ThenInclude(mg => mg.Genre)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Available = m.Available,
                Director = m.Director,
                ReleaseDate = m.ReleaseDate,
                OwnerName = m.Owner!.UserName,
                Genres = m.MovieGenres.Select(mg => mg.Genre).ToList()
            })
            .FirstOrDefaultAsync();
        
        return movie;
    }
}