namespace MovieRental_V2.Shared.Models;

public class CreateEditMovieModel
{
    public string Title { get; set; }

    public string Director { get; set; }

    public DateTime ReleaseDate { get; set; }
}