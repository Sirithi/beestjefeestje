using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Models
{
    [PrimaryKey("Id")]
    public class AnimalModel
    {
        [Key]
        public string Id { get; set; }
        [Required, StringLength(60)]
        [Display(Name = "Animal Name")]
        public string Name { get; set; }
        [Required, StringLength(31)]
        [Display(Name = "Animal Type")]
        public string AnimalName { get; set; }
        [Required, Range(0,20)]
        [Display(Name = "Cost")]
        public double Cost { get; set; }
        [Required, StringLength(400)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Animal Habitat")]
        public ATypeModel AnimalType { get; set; }
        [Required]
        public string FarmId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
