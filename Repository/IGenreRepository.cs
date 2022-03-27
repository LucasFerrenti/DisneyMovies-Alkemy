using Alkemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Repository
{
    public interface IGenreRepository
    {
        public Genre FindById(long id);
        public IQueryable<Genre> FindAll();
        public void Save(Genre genre);
        public void DeleteGenre(Genre genre);
    }
}
