

using Microsoft.AspNetCore.Identity;
using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Server.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Movie> Movies { get; set; }
}

    