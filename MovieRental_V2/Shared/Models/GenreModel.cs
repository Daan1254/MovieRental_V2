namespace MovieRental_V2.Shared.Models;

public class GenreModel
{
    public int Id { get; private set; }
    public string Title { get; set; }
    
    public ICollection<MovieGenreModel> MovieGenres { get; set; }
    
    
    public bool IsGenreLinkedToMovie()
    {
        return MovieGenres.Count > 0;
    }
}