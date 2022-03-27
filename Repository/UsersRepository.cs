using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models;

namespace Alkemy.Repository
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(DisneyMoviesContext repositoryContext) : base(repositoryContext)
        {
        }

        public User FindByEmail(string email)
        {
            return FindByCondition(user => user.Email == email).FirstOrDefault();
        }

        public User FindById(long id)
        {
            return FindByCondition(user => user.Id == id).FirstOrDefault();
        }
        public void Save(User user)
        {
            //check 
            if (user.Id == 0)
                Create(user);
            else
                Update(user);
            //save
            SaveChanges();
        }
    }
}
