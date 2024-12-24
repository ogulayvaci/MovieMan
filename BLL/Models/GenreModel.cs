using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class GenreModel
{
    public genre Record { get; set; }

    [DisplayName("Genre")]
    public string name => Record.name;
}