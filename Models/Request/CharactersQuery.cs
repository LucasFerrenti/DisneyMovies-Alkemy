using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.Request
{
    public class CharactersQuery
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public long? Movies { get; set; }
    }
}
