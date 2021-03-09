using System.Collections.Generic;
using System.Threading.Tasks;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Contracts.Persistance
{
    public interface IPersonRepository : IAsyncRepository<Person>
    {
        Task<List<Person>> GetPagedPersonsByFirstname(string firstname, int page, int size);
    }
}