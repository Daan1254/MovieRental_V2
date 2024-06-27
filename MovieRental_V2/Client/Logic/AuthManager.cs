using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Client.Logic
{
    public class AuthManager
    {
        private readonly HttpClient _httpClient = new();

        public async Task<bool> LoginAsync(LoginRegisterModel data)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7032/api/Authentication/login", data);
                
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            } 
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return false;
        }
        
        public async Task<bool> SignOutAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7032/api/Authentication/logout", new {});

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            } 
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return false;
        }
        
        public async Task<bool> RegisterAsync(LoginRegisterModel data)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7032/api/Authentication/register", data);
                
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            } 
            catch (AccessTokenNotAvailableException ex)
            {
                ex.Redirect();
            }

            return false;
        }
    }
}