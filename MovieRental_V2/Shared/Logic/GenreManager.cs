using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MovieRental_V2.Shared.Dtos;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Shared.Logic;

public class GenreManager
{
    private readonly HttpClient _httpClient;

    public GenreManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    private async Task<GenreDto> GetGenreAsync(int genreId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<GenreDto?>($"https://localhost:7032/api/Genre/{genreId}") ?? throw new InvalidOperationException();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        return null;
    }


    public async Task<CreateEditGenreDto> SetupCreateEditForm(int? GerneId)
    {
        GenreDto? genre =  await GetGenreAsync(GerneId ?? 1);

        if (genre != null)
        {
            return new CreateEditGenreDto
            {
                Title = genre.Title
            };
        }
        else
        {
            return new CreateEditGenreDto();
        }
    }
    
    
    public async Task<bool> HandleCreateEditForm(CreateEditGenreDto genre, int? genreId)
    {
        bool success = true;
        
        if (genreId == null)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7032/api/Genre", genre);
            
            if (!response.IsSuccessStatusCode)
            {
                success = false;
            }
        }
        else
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"https://localhost:7032/api/Genre/{genreId}", genre);
            Console.WriteLine(response.StatusCode);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.ReasonPhrase);
                success = false;
            }
        }

        return success;
    }
    
    public async Task<List<GenreDto>> GetGenresAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<GenreDto>>("https://localhost:7032/api/Genre") ?? throw new InvalidOperationException();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        return null;
    }
    
    public async Task<bool> DeleteGenreAsync(int genreId)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7032/api/Genre/{genreId}");

            return response.IsSuccessStatusCode;
        } catch(AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        
        
        return false;
    }
}