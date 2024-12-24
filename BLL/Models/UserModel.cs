using System.ComponentModel;
using BLL.DAL;

namespace BLL.Models;

public class UserModel
{
    public user Record { get; set; }

    [DisplayName("UserName")]
    public string username => Record.username;
    
    [DisplayName("Password")]
    public string password => Record.password;
    
    [DisplayName("User Status")]
    public string isactive => Record.isactive ? "active user" : "not active user";
    
    [DisplayName("Role")]
    public string role => Record.role?.name;
}