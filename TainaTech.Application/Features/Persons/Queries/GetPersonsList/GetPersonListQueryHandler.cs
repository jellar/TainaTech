using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Caching;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Queries.GetPersonsList
{
    
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, List<PersonListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;
        private readonly ILogger<GetPersonListQueryHandler> _logger;
        private readonly ICachedPersonsService _cachedPersonsService;

        public GetPersonListQueryHandler(IMapper mapper, IAsyncRepository<Person> personRepository,
            ILogger<GetPersonListQueryHandler> logger, ICachedPersonsService cachedPersonsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cachedPersonsService = cachedPersonsService ?? throw new ArgumentNullException(nameof(cachedPersonsService));
        }
        public async Task<List<PersonListVm>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get person list requested");

            var cachedPersons = _cachedPersonsService.GetCachedPersons();
            if (cachedPersons != null)
            {
                return _mapper.Map<List<PersonListVm>>(cachedPersons);
            }
            var persons = (await _personRepository.ListAllAsync()).OrderBy(p => p.Firstname);
            _cachedPersonsService.SetCachedPersons(persons);
            return _mapper.Map<List<PersonListVm>>(persons);

        }
    }
}
