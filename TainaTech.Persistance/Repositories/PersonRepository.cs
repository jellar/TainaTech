using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Domain.Entities;

namespace TainaTech.Persistance.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(PeopleDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Person>> GetPagedPersonsByFirstname(string firstname, int page, int size)
        {
            if (string.IsNullOrEmpty(firstname))
            {
                return await _dbContext.Persons
                    .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
            }
            return await _dbContext.Persons.Where(p => p.Firstname.Contains(firstname))
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }
    }
}