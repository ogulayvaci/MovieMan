using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class role
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(5)]
    public string name { get; set; }

    [InverseProperty("role")]
    public virtual ICollection<user> user { get; set; } = new List<user>();
}
