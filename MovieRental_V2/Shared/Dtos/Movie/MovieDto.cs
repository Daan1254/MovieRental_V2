using MovieRental_V2.Shared.Models;

namespace MovieRental_V2.Shared.Dtos;

public class MovieDto
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    
    public MovieState State { get; set; }
    public string Director { get; private set; }
    public DateTime ReleaseDate { get; set; }
    public string? OwnerName { get; set; }

    public List<GenreModel> Genres { get; set; }


    public MovieDto(int id, string title, string director)
    {
        this.Id = id;
        this.Title = title;
        this.Director = director;
    }
}