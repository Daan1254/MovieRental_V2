using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Shared.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Available { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? OwnerName { get; set; }

    public List<GenreModel> Genres { get; set; }
}