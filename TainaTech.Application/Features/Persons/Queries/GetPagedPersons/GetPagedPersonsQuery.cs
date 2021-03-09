using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace TainaTech.Application.Features.Persons.Queries.GetPagedPersons
{
    public class GetPagedPersonsQuery : IRequest<PagedPersonsVm>
    {
        public string Firstname { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
