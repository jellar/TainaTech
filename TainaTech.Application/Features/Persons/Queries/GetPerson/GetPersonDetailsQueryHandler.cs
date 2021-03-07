using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Application.Exceptions;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Queries.GetPerson
{
    public class GetPersonDetailsQueryHandler : IRequestHandler<GetPersonDetailsQuery, PersonDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;
        private readonly ILogger<GetPersonDetailsQueryHandler> _logger;
        public GetPersonDetailsQueryHandler(IMapper mapper, IAsyncRepository<Person> personRepository,
            ILogger<GetPersonDetailsQueryHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<PersonDetailsVm> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if(person == null)
            {
                _logger.LogError($"Get person: Not found exception raised. Person Id: {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }
            return _mapper.Map<PersonDetailsVm>(person);
        }
    }
}
