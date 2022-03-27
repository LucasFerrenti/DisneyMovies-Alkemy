using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.DTO
{
    public class MovieDTO
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long GenreId { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public int Qualification { get; set; }
        public GenreDTO Genre { get; set; }
        public List<CharacterViewDTO> Characters { get; set; }
    }
}
