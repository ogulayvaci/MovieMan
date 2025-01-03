﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class director
{
    [Key]
    public int id { get; set; }

    [Required]
    [StringLength(100)]
    public string name { get; set; }

    [Required]
    [StringLength(100)]
    public string surname { get; set; }

    public bool isretired { get; set; }

    [InverseProperty("director")]
    public virtual ICollection<movie> movie { get; set; } = new List<movie>();
}
