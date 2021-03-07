using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using TainaTech.Application.Contracts;
using TainaTech.Domain.Entities;
using Xunit;

namespace TainaTech.Persistance.IntegrationTests
{
    public class PeopleDbContextTests
    {
        private readonly PeopleDbContext _peopleDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public PeopleDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<PeopleDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _peopleDbContext = new PeopleDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var person = new Person() { PersonId = 1, Firstname = "Test Firstname" };

            _peopleDbContext.Persons.Add(person);
            await _peopleDbContext.SaveChangesAsync();

            person.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}
