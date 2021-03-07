using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using TainaTech.Application.Contracts;
using TainaTech.Domain.Entities;
using Xunit;

namespace TainaTech.Persistance.UnitTests
{
    public class PeopleDbContextTests
    {
        private readonly PeopleDbContext _peopleDbContext;       

        public PeopleDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<PeopleDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
                
            _peopleDbContext = new PeopleDbContext(dbContextOptions);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var person = new Person() { PersonId = 1, Firstname = "Test Firstname" };

            _peopleDbContext.Persons.Add(person);
            await _peopleDbContext.SaveChangesAsync();

            person.PersonId.ShouldBe(1);
        }
    }
}
