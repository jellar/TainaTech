using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TainaTech.Application.Features.Persons.Queries.GetPerson
{
    public class GetPersonDetailsQuery : IRequest<PersonDetailsVm>
    {
        public long PersonId { get; set; }
    }
}
