using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MovieRental_V2.Shared.Dtos;

namespace MovieRental_V2.Shared.Logic;

public class MovieManager
{
    private readonly HttpClient _httpClient;

    public MovieManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<MovieDto>> GetMoviesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<MovieDto>>("https://localhost:7032/api/Movie") ?? throw new InvalidOperationException();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        return null;
    }
    
    
    public async Task<bool> RentMovieAsync(int movieId)
    {
        bool success = true;
        
        var movie = await _httpClient.PutAsync($"https://localhost:7032/api/Movie/{movieId}/rent",null);
        
        if (!movie.IsSuccessStatusCode)
        {
            success = false;
        }

        return success;
    }
    
    public async Task<bool> ReturnMovieAsync(int movieId)
    {
        bool success = true;
        
        var movie = await _httpClient.PutAsync($"https://localhost:7032/api/Movie/{movieId}/return",null);
        
        if (!movie.IsSuccessStatusCode)
        {
            success = false;
        }

        return success;
    }
    
    public async Task<MovieDto?> GetMovieAsync(int? movieId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<MovieDto?>($"https://localhost:7032/api/Movie/{movieId}") ?? throw new InvalidOperationException();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        return null;
    }


    public async Task<CreateEditMovieDto> SetupCreateEditForm(int? MovieId)
    {
        if (MovieId != null)
        {
            MovieDto? movie =  await GetMovieAsync(MovieId);

            if (movie != null)
            {
                return new CreateEditMovieDto()
                {
                    Title = movie.Title,
                    Director = movie.Director,
                    ReleaseDate = movie.ReleaseDate,
                };

            }
            else
            {
                return new CreateEditMovieDto();
            }
        }
        else
        { 
            return new CreateEditMovieDto();
        }
    }
    
    public async Task<bool> HandleCreateEditForm(CreateEditMovieDto movie, int? movieId)
    {
        bool success = true;
        
        if (movieId == null)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7032/api/Movie", movie);
            
            if (!response.IsSuccessStatusCode)
            {
                success = false;
            }
        }
        else
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7032/api/Movie/{movieId}", movie);
            Console.WriteLine(response.StatusCode);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.ReasonPhrase);
                success = false;
            }
        }

        return success;
    }
    
    public async Task<bool> DeleteMovieAsync(int movieId)
    {
        bool success = true;
        
        var movie = await _httpClient.DeleteAsync($"https://localhost:7032/api/Movie/{movieId}");
        
        if (!movie.IsSuccessStatusCode)
        {
            success = false;
        }

        return success;
    }
}