using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

[Index("name", Name = "genre_name_key", IsUnique = true)]
public partial class genre
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(50)]
    public string name { get; set; }

    [InverseProperty("genre")]
    public virtual ICollection<moviegenre> moviegenre { get; set; } = new List<moviegenre>();
}
