using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Application.Exceptions;
using TainaTech.Domain.Entities;

namespace TainaTech.Application.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Person> _personRepository;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;

        public UpdatePersonCommandHandler(IMapper mapper, IAsyncRepository<Person> personRepository, ILogger<UpdatePersonCommandHandler> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var personToUpdate = await _personRepository.GetByIdAsync(request.PersonId);

            if (personToUpdate == null)
            {
                _logger.LogError($"Update person: Not found exception raised. Person Id: {request.PersonId}");
                throw new NotFoundException(nameof(Person), request.PersonId);
            }

            var validator = new UpdatePersonCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, personToUpdate, typeof(UpdatePersonCommand), typeof(Person));

            try
            {
                await _personRepository.UpdateAsync(personToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update person failed due to an error. {ex.Message}");
            }

            return Unit.Value;

        }

    }
}
