using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental_V2.Server.Data;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public MovieController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {
        return Ok(await _context.Movies.ToListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }
        
        return Ok(movie);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMovie(CreateEditMovie movie)
    {
        
        _context.Movies.Add(new Movie
        {
            
            Title = movie.Title,
            Available = movie.Available,
            Director = movie.Director,
            ReleaseDate = DateTime.Now
        });
        
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, CreateEditMovie movie)
    {
        Movie? existingMovie = await _context.Movies.FindAsync(id);
        
        if (existingMovie == null)
        {
            return NotFound();
        }
        
        existingMovie.Title = movie.Title;
        existingMovie.Available = movie.Available;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        Movie? movie = await _context.Movies.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }
        
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}