using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Queries.GetPersonsList
{
    
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, List<PersonListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;

        public GetPersonListQueryHandler(IMapper mapper, IAsyncRepository<Person> personRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));           
        }

        public async Task<List<PersonListVm>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {            
            var persons = (await _personRepository.ListAllAsync()).OrderBy(p => p.Firstname);
           
            return _mapper.Map<List<PersonListVm>>(persons);
        }
    }
}
