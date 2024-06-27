namespace MovieRental_V2.Shared.Logic;

public class Movie
{
    public int Id { get; private set; }  
    
    public string Title { get; set; }
    
    public bool Available { get; private set; }
    
    public string Director { get; private set; }
    
    public DateTime ReleaseDate { get; set; }

    public List<string> Genres { get; set; }


    public Movie()
    {

        Available = true;
    }
}