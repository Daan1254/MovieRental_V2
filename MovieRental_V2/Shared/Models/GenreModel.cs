namespace MovieRental_V2.Shared.Models;

public class GenreModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    
    public ICollection<MovieGenreModel> MovieGenres { get; set; }


    public GenreModel(string? title)
    {
        this.Title = title;
    }
    
    public void SetTitle(string title)
    {
        if (!string.IsNullOrEmpty(title))
        {
            this.Title = title;
        }
    }
}