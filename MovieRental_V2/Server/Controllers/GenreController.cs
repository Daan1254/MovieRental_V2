using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRental_V2.Server.Data;
using MovieRental_V2.Server.Services;
using MovieRental_V2.Shared.Dtos;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly GenreService _genreService;


    public GenreController(ApplicationDbContext context)
    {
        _context = context;
        
        _genreService = new GenreService(_context);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        List<GenreDto> genres = await _genreService.GetGernes();
            
        
        return Ok(genres);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre([FromRoute] int id)
    {
        GenreDto? genre = await _genreService.GetGenre(id);
        
        if (genre == null)
        {
            return NotFound();
        }
        
        return Ok(genre);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] CreateEditGenreDto createEditGenreDto)
    {
        _context.Genres.Add(new GenreModel()
        {
            Title = createEditGenreDto.Title!
        });
        
        await _context.SaveChangesAsync();

        return Ok(createEditGenreDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditGenre(int id, [FromBody] CreateEditGenreDto createEditGenreDto)
    {
        GenreModel? genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        genre.Title = createEditGenreDto.Title!;
            
        await _context.SaveChangesAsync();

        return Ok(createEditGenreDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        GenreModel? genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        bool isLinked = genre.IsGenreLinkedToMovie();

        if (isLinked)
        {
            BadRequest();
        }

        _context.Genres.Remove(genre);
        
        await _context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> AddMovieToGenre([FromBody] CreateDeleteGenreDto createDeleteGenreDto)
    {
        GenreModel? genre = await _context.Genres.FindAsync(createDeleteGenreDto.GenreId);
        MovieModel? movie = await _context.Movies.FindAsync(createDeleteGenreDto.MovieId);
        
        
        if (genre == null || movie == null)
        {
            return NotFound();
        }
        
        MovieGenreModel? movieGenre = _context.MovieGenres.FirstOrDefault(mg => mg.MovieId == createDeleteGenreDto.MovieId && mg.GenreId == createDeleteGenreDto.GenreId);

        if (movieGenre != null)
        {
            return BadRequest("Movie already has this genre.");
        }
        
        _context.MovieGenres.Add(new MovieGenreModel(createDeleteGenreDto.MovieId, createDeleteGenreDto.GenreId));
        
        await _context.SaveChangesAsync();

        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveMovieFromGenre([FromBody] AddGenreToMovieDto addGenreToMovieDto)
    {
        GenreModel? genre = await _context.Genres.FindAsync(addGenreToMovieDto.GenreId);
        MovieModel? movie = await _context.Movies.FindAsync(addGenreToMovieDto.MovieId);

        if (genre == null || movie == null)
        {
            return NotFound();
        }

        MovieGenreModel? movieGenre = _context.MovieGenres.FirstOrDefault(mg => mg.MovieId == addGenreToMovieDto.MovieId && mg.GenreId == addGenreToMovieDto.GenreId);

        if (movieGenre == null)
        {
            return NotFound();
        }

        genre.MovieGenres.Remove(movieGenre);
        
        await _context.SaveChangesAsync();

        return Ok();
    }
    
}