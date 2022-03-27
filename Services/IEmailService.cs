using Alkemy.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Services
{
    public interface IEmailService
    {
        public void Send(EmailContent emailContent);
        public Task SendAsync(EmailContent emailContent);
    }
}
