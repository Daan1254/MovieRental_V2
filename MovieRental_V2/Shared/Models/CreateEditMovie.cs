namespace MovieRental_V2.Shared.Models;

public class CreateEditMovie
{
    public string Title { get; set; }
    public bool Available { get; set; }

    public string Director { get; set; }

    public DateTime? ReleaseDate { get; set; }
}