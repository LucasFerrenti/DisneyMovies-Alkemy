using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.DTO
{
    public class CharacterDTO
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }
        public double? Weight { get; set; }
        [Required]
        public string History { get; set; }
        public List<MovieViewDTO> Movies { get; set; }
    }
}
