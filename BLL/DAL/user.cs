using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Index("username", Name = "user_username_key", IsUnique = true)]
public partial class user
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string username { get; set; } = null!;

    [StringLength(255)]
    public string password { get; set; } = null!;

    public bool isactive { get; set; }

    public int? roleid { get; set; }

    [ForeignKey("roleid")]
    [InverseProperty("user")]
    public virtual role? role { get; set; }
}
