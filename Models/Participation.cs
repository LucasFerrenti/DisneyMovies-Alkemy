using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class Participation
    {
        public long CharacterId { get; set; }
        public long MovieId { get; set; }
        public Character Character { get; set; }
        public Movie Movie { get; set; }
    }
}
