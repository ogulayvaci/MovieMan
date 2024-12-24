using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class movie
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(200)]
    public string name { get; set; }

    public DateOnly? releasedate { get; set; }

    [Precision(15, 2)]
    public decimal? totalrevenue { get; set; }

    public int directorid { get; set; }

    [ForeignKey("directorid")]
    [InverseProperty("movie")]
    public virtual director director { get; set; }

    [InverseProperty("movie")]
    public virtual ICollection<moviegenre> moviegenre { get; set; } = new List<moviegenre>();
}
