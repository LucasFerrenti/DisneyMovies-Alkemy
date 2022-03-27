using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models;

namespace Alkemy.Repository
{
    public interface IUsersRepository
    {
        public User FindById(long id);
        public User FindByEmail(string email);
        public void Save(User user);
    }
}
