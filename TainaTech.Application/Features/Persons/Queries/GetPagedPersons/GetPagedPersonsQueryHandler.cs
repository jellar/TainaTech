using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Queries.GetPagedPersons
{
    public class GetPagedPersonsQueryHandler : IRequestHandler<GetPagedPersonsQuery, PagedPersonsVm>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<GetPagedPersonsQueryHandler> _logger;
        public GetPagedPersonsQueryHandler(IMapper mapper, IPersonRepository personRepository,
            ILogger<GetPagedPersonsQueryHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<PagedPersonsVm> Handle(GetPagedPersonsQuery request, CancellationToken cancellationToken)
        {
            var list = await _personRepository.GetPagedPersonsByFirstname(request.Firstname, request.Page, request.Size);
            var persons = _mapper.Map<List<PersonListVm>>(list);
            
            var actualPersonsCount = (await _personRepository.ListAllAsync()).Count;
            if (!string.IsNullOrEmpty(request.Firstname))
            {
                actualPersonsCount = (await _personRepository.ListAllAsync()).Count(p => p.Firstname == request.Firstname);
            }
            return new PagedPersonsVm() 
                { Page = request.Page, Size = request.Size, Count = actualPersonsCount, Persons = persons };

        }
    }
}
