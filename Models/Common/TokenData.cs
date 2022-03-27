using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alkemy.Models.Common
{
    public class TokenData
    {
        [Required]
        public Claim[] Claims { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
    }
}
