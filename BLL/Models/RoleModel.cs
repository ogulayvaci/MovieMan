using BLL.DAL;

namespace BLL.Models;

public class RoleModel
{
    public role Record { get; set; }
    
    public string name => Record.name;
    public string users => string.Join("<br>", Record.user?.Select(u => u.role));
}