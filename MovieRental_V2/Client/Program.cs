using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieRental_V2.Client;
using MovieRental_V2.Client.Logic;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("MovieRental_V2.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MovieRental_V2.ServerAPI"));
builder.Services.AddScoped<AuthManager>();


builder.Services.AddApiAuthorization(options =>
{
    options.AuthenticationPaths.LogInPath = "login";
    options.AuthenticationPaths.RegisterPath = "register";
    options.AuthenticationPaths.ProfilePath = "FetchData";
});

await builder.Build().RunAsync();

