﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Models
{
    public class ATypeModel
    {
        [Required]
        public string Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

    }
}
