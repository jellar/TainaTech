using System.Collections.Generic;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;

namespace TainaTech.Application.Features.Persons.Queries.GetPagedPersons
{
    public class PagedPersonsVm
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ICollection<PersonListVm> Persons { get; set; }
    }
}
