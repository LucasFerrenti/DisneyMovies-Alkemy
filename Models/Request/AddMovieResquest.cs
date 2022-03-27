using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.Request
{
    public class AddMovieResquest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        [Range(0,5)]
        public int Qualification { get; set; }
        public long? GenreId { get; set; }
        public ICollection<long> CharactersId { get; set; }
    }
}
