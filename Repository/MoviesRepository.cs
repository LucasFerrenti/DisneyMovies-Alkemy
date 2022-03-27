using Alkemy.Models;
using Alkemy.Models.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Repository
{
    public class MoviesRepository : RepositoryBase<Movie>, IMoviesRepository
    {
        public MoviesRepository(DisneyMoviesContext repositoryContext) : base(repositoryContext)
        {
        }
        public ICollection<Movie> FindQuery(MoviesQuery request)
        {
            bool findByName = !String.IsNullOrEmpty(request.Name);
            bool findByGenre = request.Genre != null;
            bool orderByAsc = request.Order != "DESC";

            return orderByAsc ?
                 //desc case
                FindByCondition(m =>
                (!findByName || m.Title == request.Name) &&
                (!findByGenre || m.GenreId == request.Genre))
                .OrderBy(m => m.CreationDate).ToList()
                ://asc default
                FindByCondition(m =>
                (!findByName || m.Title == request.Name) &&
                (!findByGenre || m.GenreId == request.Genre))
                .OrderByDescending(m => m.CreationDate).ToList();
        }
        public Movie FindById(long id)
        {
            return FindByCondition(m => m.Id == id).FirstOrDefault();
        }
        public Movie FindByIdWithCharacters(long id)
        {
            return FindByCondition(
                m => m.Id == id,
                m => m
                    .Include(m => m.Participations)
                        .ThenInclude(part => part.Character)
                    .Include(m => m.Genre))
                .FirstOrDefault();
        }
        public void Save(Movie movie)
        {
            //check 
            if (movie.Id == 0)
                Create(movie);
            else
                Update(movie);
            //save
            SaveChanges();
        }

        public void DeleteMovie(Movie movie)
        {
            Delete(movie);
            SaveChanges();
        }
    }
}
