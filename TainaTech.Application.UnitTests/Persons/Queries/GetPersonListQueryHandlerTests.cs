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
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;
using TainaTech.Application.Profiles;
using TainaTech.Application.UnitTests.Mocks;
using TainaTech.Domain.Entities;
using Xunit;

namespace TainaTech.Application.UnitTests.Persons.Queries
{
    public class GetPersonListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Person>> _mockPersonRepository;
        private readonly Mock<ILogger<GetPersonListQueryHandler>> _mockLogger;
        private readonly Mock<ICachedPersonsService> _cacheService;
        public GetPersonListQueryHandlerTests()
        {
            _mockPersonRepository = RepositoryMocks.GetPersonRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _mockLogger = new Mock<ILogger<GetPersonListQueryHandler>>();
            _cacheService = new Mock<ICachedPersonsService>();
        }

        [Fact]
        public async Task GetPersonListTest()
        {
            var handler = new GetPersonListQueryHandler(_mapper, _mockPersonRepository.Object, _mockLogger.Object, _cacheService.Object);

            var result = await handler.Handle(new GetPersonListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<PersonListVm>>();

            result.Count.ShouldBe(2);
        }
    }
}
