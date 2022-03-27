using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.Request
{
    public class AddCharacterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public double Weigth { get; set; }
        [Required]
        public string Historty { get; set; }
        public ICollection<long> MoviesId { get; set; }
    }
}
