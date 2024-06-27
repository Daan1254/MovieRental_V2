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
        List<GenreDto> genres = await _context.Genres
            .Select(g => new GenreDto(g.Id, g.Title))
            .ToListAsync();

        return genres;  
    }

    public async Task<GenreDto?> GetGenre(int Id)
    {
        GenreDto? genre = await _context.Genres
            .Where(g => g.Id == Id)
            .Select(g => new GenreDto(g.Id, g.Title))
            .FirstOrDefaultAsync();
        
        return genre;
    }
}