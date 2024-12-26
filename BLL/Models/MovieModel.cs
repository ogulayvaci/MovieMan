using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class MovieModel
{
    public movie Record { get; set; }

    [DisplayName ("Movie Name")]
    public string name => Record.name;
    
    [DisplayName ("Release Date")]
    public string releasedate => Record.releasedate.HasValue ? Record.releasedate.Value.ToString("MM/dd/yyyy") : string.Empty;
    
    [DisplayName ("Total Revenue")]
    public string totalrevenue => Record.totalrevenue.HasValue ? Record.totalrevenue.Value.ToString("C2") : string.Empty;

    [DisplayName ("Movie Director")]
    public string director => Record.director?.name + " " + Record.director?.surname;
    
    [DisplayName ("Genres")]
    public string genres => string.Join("<br>", Record.moviegenre?.Select(g => g.genre?.name));
    
    [DisplayName ("Movie Genres")]
    public List<int> genreids
    {
        get => Record.moviegenre?.Select(mg => mg.genreid).ToList();
        set => Record.moviegenre = value.Select(v=> new moviegenre() {genreid = v}).ToList();
    }

}