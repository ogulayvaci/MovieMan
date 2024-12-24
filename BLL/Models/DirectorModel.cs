using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class DirectorModel
{
    public director Record { get; set; }

    [DisplayName("Director Name")]
    public string name => Record.name;
    
    [DisplayName("Director Surname")]
    public string surname => Record.surname;
    
    [DisplayName("Director Status")]
    public string isretired => Record.isretired ? "Yes" : "No";
    
    [DisplayName("Director's Movies")]
    public string movies => string.Join("<br>", Record.movie?.Select(m => m.name));

    public string Developer { get; set; }
}