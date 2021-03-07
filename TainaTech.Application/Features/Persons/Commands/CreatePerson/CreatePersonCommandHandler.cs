using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Caching;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Application.Exceptions;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;
        private readonly ILogger<CreatePersonCommandHandler> _logger;
        private readonly ICachedPersonsService _cachedPersonsService;

        public CreatePersonCommandHandler(IMapper mapper, IAsyncRepository<Person> personRepository, 
            ILogger<CreatePersonCommandHandler> logger, ICachedPersonsService cachedPersonsService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cachedPersonsService = cachedPersonsService ?? throw new ArgumentNullException(nameof(cachedPersonsService));
        }
        public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var personCommandResponse = new CreatePersonCommandResponse();

            var validator = new CreatePersonCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                personCommandResponse.Success = false;
                personCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    personCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }                
            }

            if (personCommandResponse.Success)
            {
                var person = _mapper.Map<Person>(request);
                try
                {
                    person = await _personRepository.AddAsync(person);
                    _cachedPersonsService.DeleteCachedPersons();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Create person failed due to an error. {ex.Message}");
                    throw new Exception(nameof(Person));
                }

                personCommandResponse.Person = _mapper.Map<PersonDto>(person);
            }


            return personCommandResponse;
        }

    }
}
