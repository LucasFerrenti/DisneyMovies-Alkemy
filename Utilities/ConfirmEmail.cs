using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Utilities.ConfirmEmail
{
    public class ConfirmEmail
    {
        public static string CreateBody(string token, string email,string domain)
        {
            var body = System.IO.File.ReadAllText(@"..\Alkemy\wwwroot\ConfirmEmail.html");
            return body.Replace("@user", email).Replace("@domain", domain).Replace("@token", token);
        }
    }
}
