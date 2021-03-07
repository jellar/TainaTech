using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts.Caching;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Application.Features.Persons.Commands.CreatePerson;
using TainaTech.Application.Profiles;
using TainaTech.Application.UnitTests.Mocks;
using TainaTech.Domain.Entities;
using TainaTech.Domain.Enums;
using Xunit;

namespace TainaTech.Application.UnitTests.Persons.Commands
{
    public class CreatePersonTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Person>> _mockPersonRepository;
        private readonly Mock<ILogger<CreatePersonCommandHandler>> _mockLogger;
        private readonly Mock<ICachedPersonsService> _cacheService;
        public CreatePersonTests()
        {
            _mockPersonRepository = RepositoryMocks.GetPersonRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _mockLogger = new Mock<ILogger<CreatePersonCommandHandler>>();
            _cacheService = new Mock<ICachedPersonsService>();
        }

        [Fact]
        public async Task Handle_ValidPerson_AddedToPersonsRepo()
        {
            var handler = new CreatePersonCommandHandler(_mapper, _mockPersonRepository.Object, _mockLogger.Object, _cacheService.Object);

            var newPerson = new CreatePersonCommand
            {
                Firstname = "Test Firstname",
                Surname = "Test Surname",
                Gender = Gender.Male,
                EmailAddress = "Test Email",
                PhoneNumber = "Test Phonenumber",
                DateOfBirth = DateTime.Now.AddYears(-30)
            };

            await handler.Handle(newPerson, CancellationToken.None);

            var allPersons = await _mockPersonRepository.Object.ListAllAsync();

            allPersons.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_InValidPerson_ShouldReturnValidationErrors()
        {
            var handler = new CreatePersonCommandHandler(_mapper, _mockPersonRepository.Object, _mockLogger.Object, _cacheService.Object);

            var newPerson = new CreatePersonCommand
            {
                Firstname = "Test Firstname"
            };

            CreatePersonCommandResponse response = await handler.Handle(newPerson, CancellationToken.None);

            response.ValidationErrors.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Handle_FutureDateOfBirth_ShouldReturnValidationErrors()
        {
            var handler = new CreatePersonCommandHandler(_mapper, _mockPersonRepository.Object, _mockLogger.Object, _cacheService.Object);

            var newPerson = new CreatePersonCommand
            {
                Firstname = "Test Firstname",
                Surname = "Test Surname",
                Gender = Gender.Male,
                EmailAddress = "Test Email",
                PhoneNumber = "Test Phonenumber",
                DateOfBirth = DateTime.Now.AddDays(1)
            };

            CreatePersonCommandResponse response = await handler.Handle(newPerson, CancellationToken.None);

            response.ValidationErrors.Count.ShouldBeGreaterThan(0);
        }
    }
}
