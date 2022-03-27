using Alkemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Repository
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(DisneyMoviesContext repositoryContext) : base(repositoryContext)
        {
        }

        public void DeleteGenre(Genre genre)
        {
            Delete(genre);
            SaveChanges();
        }

        public Genre FindById(long id)
        {
            return FindByCondition(gen => gen.Id == id).FirstOrDefault();
        }
        public void Save(Genre genre)
        {
            //check
            if (genre.Id == 0)
                Create(genre);
            else
                Update(genre);
            //save
            SaveChanges();
        }
    }
}
