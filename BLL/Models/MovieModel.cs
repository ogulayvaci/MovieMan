using BLL.DAL;

namespace BLL.Models;

public class MovieModel
{
    public movie Record { get; set; }

    public string name => Record.name;
    
    public string releasedate => Record.releasedate.HasValue ? Record.releasedate.Value.ToString("MM/dd/yyyy") : string.Empty;
    
    public string totalrevenue => Record.totalrevenue.HasValue ? Record.totalrevenue.ToString() : string.Empty;

    public string director => Record.director?.name;
    
    public string moviegenres => string.Join("<br>", Record.moviegenre?.Select(g => g.movie?.name));

}