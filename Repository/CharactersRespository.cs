using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models;
using Alkemy.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace Alkemy.Repository
{
    public class CharactersRespository : RepositoryBase<Character>, ICharactersRepository
    {
        public CharactersRespository(DisneyMoviesContext repositoryContext) : base(repositoryContext)
        {
        }

        public Character FindById(long Id)
        {
            return FindByCondition(chart => chart.Id == Id).FirstOrDefault();
        }
        public Character FindByIdWithMovies(long Id)
        {
            return FindByCondition(chart => chart.Id == Id,
                chart => chart.Include(chart => chart.Participations)
                        .ThenInclude(part => part.Movie))
                .FirstOrDefault();
        }
        public ICollection<Character> FindQuerry(CharactersQuery request)
        {
            bool filterByName = !String.IsNullOrEmpty(request.Name);
            bool filterByAge = request.Age != null;
            bool filterByMovie = request.Movies != null;

            return FindByCondition(p =>
                (!filterByName || p.Name == request.Name) &&
                (!filterByAge || p.Age == request.Age) &&
                (!filterByMovie || p.Participations.Any(part => part.MovieId == request.Movies)))
                .OrderBy(p => p.Name)
                .ToList();
        }

        public void Save(Character character)
        {
            //check 
            if (character.Id == 0)
                Create(character);
            else
                Update(character);
            //save
            SaveChanges();
        }

        public void DeleteCharacter(Character character)
        {
            Delete(character);
            SaveChanges();
        }
    }
}
