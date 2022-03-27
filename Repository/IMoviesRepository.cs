using Alkemy.Models;
using Alkemy.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Repository
{
    public interface IMoviesRepository
    {
        
        public ICollection<Movie> FindQuery(MoviesQuery request);
        public Movie FindById(long id);
        public Movie FindByIdWithCharacters(long id);
        public void Save(Movie movie);
        public void DeleteMovie(Movie movie);
        public void SaveChanges();
    }
}
