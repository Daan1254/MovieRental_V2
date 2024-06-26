using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental_V2.Server.Data;
using MovieRental_V2.Shared.Dtos;

namespace MovieRental_V2.Server.Services;

public class GenreService
{
    
    private readonly ApplicationDbContext _context;
    
    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<GenreDto>> GetGernes()
    {
        List<GenreDto> movies = await _context.Genres
            .Select(g => new GenreDto(g.Id, g.Title))
            .ToListAsync();

        return movies;  
    }

    public async Task<GenreDto?> GetGenre()
    {
        GenreDto? movie = await _context.Genres
            .Select(g => new GenreDto(g.Id, g.Title))
            .FirstOrDefaultAsync();
        
        return movie;
    }
}