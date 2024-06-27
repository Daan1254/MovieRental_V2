

using MovieRental_V2.Server.Models;


public enum MovieState
{
    AVAILABLE,
    RENTED,
    UNAVAILABLE
}

namespace MovieRental_V2.Shared.Models
{
    public class MovieModel
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        
        public MovieState State { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        // Foreign Key
        public string? OwnerId { get; set; }

        // Navigation Property
        public ApplicationUser? Owner { get; set; }
        
        public ICollection<MovieGenreModel> MovieGenres { get; set; }
    }
}