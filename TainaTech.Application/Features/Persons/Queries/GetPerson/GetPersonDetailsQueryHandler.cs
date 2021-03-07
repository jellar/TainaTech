using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Queries.GetPerson
{
    public class GetPersonDetailsQueryHandler : IRequestHandler<GetPersonDetailsQuery, PersonDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;

        public GetPersonDetailsQueryHandler(IMapper mapper, IAsyncRepository<Person> personRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<PersonDetailsVm> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            return _mapper.Map<PersonDetailsVm>(person);
        }
    }
}
