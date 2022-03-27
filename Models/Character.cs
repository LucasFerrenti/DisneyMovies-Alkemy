using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class Character
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public double? Weigth { get; set; }
        public string History { get; set; }
        public ICollection<Participation> Participations { get; set; }
    }
}
