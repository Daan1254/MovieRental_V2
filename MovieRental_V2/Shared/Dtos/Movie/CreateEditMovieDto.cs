namespace MovieRental_V2.Shared.Dtos;

public class CreateEditMovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
}