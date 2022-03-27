using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models.Common;
using System.Security.Claims;

namespace Alkemy.Services
{
    public interface IAuthService
    {
        public string CreateToken(TokenData data);
    }
}
