

using MovieRental_V2.Server.Models;

namespace MovieRental_V2.Shared.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        // Foreign Key
        public string OwnerId { get; set; }

        // Navigation Property
        public ApplicationUser Owner { get; set; }
    }
}