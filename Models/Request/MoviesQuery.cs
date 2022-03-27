using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.Request
{
    public class MoviesQuery
    {
        public string Name { get; set; }
        public long? Genre { get; set; }
        public string Order { get; set; }
    }
}
