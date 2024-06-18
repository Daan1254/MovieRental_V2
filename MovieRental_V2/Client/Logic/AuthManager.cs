using System.Net.Http.Json;
using MovieRental_V2.Client.Pages;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Client.Managers;

public class AuthManager
{
    private readonly HttpClient _http = new HttpClient();
    
    public AuthManager()
    {
        
    }

    private class LoginResult
    {
        public bool Successful { get; set; }
    }
    
    public bool Login(LoginModel data)
    {
        HttpResponseMessage response = _http.PostAsJsonAsync("api/Authentication/Login", data).Result;
        
        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadFromJsonAsync<LoginResult>().Result;
            if (result.Successful)
            {
                return true;
            }
        }
        return false;
    }
}