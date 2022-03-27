using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public long? GenreId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Qualification { get; set; }
        public ICollection<Participation> Participations { get; set; }
        public Genre Genre { get; set; }
    }
}
