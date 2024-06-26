using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental_V2.Server.Data;
using MovieRental_V2.Server.Services;
using MovieRental_V2.Shared.Dtos;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Controllers;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    private readonly MovieService _movieService;
    
    public MovieController(ApplicationDbContext context)
    {
        _context = context;
        
        _movieService = new MovieService(_context);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMovies()
    {

        List<MovieDto> movies = await _movieService.GetMovies();

        return Ok(movies);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        MovieDto? movie = await _movieService.GetMovie();
        
        if (movie == null)
        {
            return NotFound();
        }
        
        return Ok(movie);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMovie(CreateEditMovieModel movieModel)
    {
        
        _context.Movies.Add(new MovieModel
        {
            
            Title = movieModel.Title,
            Available = true,
            Director = movieModel.Director,
            ReleaseDate = DateTime.Now
        });
        
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie([FromRoute] int id, [FromBody] CreateEditMovieModel movieModel)
    {
        MovieModel? existingMovie = await _context.Movies.FindAsync(id);
        
        if (existingMovie == null)
        {
            return NotFound();
        }
        
        existingMovie.Title = movieModel.Title;
        existingMovie.Director = movieModel.Director;
        existingMovie.ReleaseDate = movieModel.ReleaseDate;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    // [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        MovieModel? movie = await _context.Movies.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }
        
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpPut("{id}/rent")]
    public async Task<IActionResult> RentMovie(int id)
    {
        MovieModel? movie = await _context.Movies.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }
        
        movie.Available = false;
        
        Claim? claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        
        if (claim == null)
        {
            return Unauthorized();
        }
        
        movie.OwnerId = claim.Value;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}