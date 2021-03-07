using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TainaTech.Application.Features.Persons.Queries.GetPersonsList
{
    public class GetPersonListQuery : IRequest<List<PersonListVm>>
    {
    }
}
