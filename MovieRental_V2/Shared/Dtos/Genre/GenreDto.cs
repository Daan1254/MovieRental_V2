namespace MovieRental_V2.Shared.Dtos;

public class GenreDto
{
    public int Id { get; private set; }
    public string Title { get; private set; }


    public GenreDto(int id, string title)
    {
        this.Id = id;
        this.Title = title;
    }
}