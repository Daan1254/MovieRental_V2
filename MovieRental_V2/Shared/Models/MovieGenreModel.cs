namespace MovieRental_V2.Shared.Models;

public class MovieGenreModel
{
    public int MovieId { get; private set; }
    public MovieModel? Movie { get;  set; }

    public int GenreId { get; private set; }
    public GenreModel? Genre { get; private set; }
    
    
    public MovieGenreModel(int movieId, int genreId)
    {
        this.MovieId = movieId;
        this.GenreId = genreId;
    }
}