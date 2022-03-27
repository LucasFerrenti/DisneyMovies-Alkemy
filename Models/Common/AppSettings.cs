using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models.Common
{
    public class AppSettings
    {
        public string JwtKey { get; set; }
        public EmailClient EmailClient{ get; set; }
    }
}
