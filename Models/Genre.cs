using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
