using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alkemy.Models;
using Alkemy.Models.Request;

namespace Alkemy.Repository
{
    public interface ICharactersRepository
    {
        public ICollection<Character> FindQuerry(CharactersQuery request);
        public Character FindById(long Id);
        public Character FindByIdWithMovies(long Id);
        public void Save(Character character);
        public void DeleteCharacter(Character character);

    }
}
