using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BLL.DAL;

namespace BLL.Models;

public class UserModel
{
    public user Record { get; set; }
    
    [DisplayName("UserName")]
    public string username => Record.username;
    
    [DisplayName("Password")]
    public string password => Record.password;
    
    public string isactive => Record.isactive ? "active" : "not active";
    
    public string role => Record.role?.name;
}