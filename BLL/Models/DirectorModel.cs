using BLL.DAL;

namespace BLL.Models;

public class DirectorModel
{
    public director Record { get; set; }

    public string name => Record.name;
    public string surname => Record.surname;
    public string isretired => Record.isretired ? "Yes" : "No";
    public string movies => string.Join("<br>", Record.movie?.Select(m => m.name));

    public string Developer { get; set; }
}