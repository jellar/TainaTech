using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Domain.Entities;
using TainaTech.Domain.Enums;

namespace TainaTech.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Person>> GetPersonRepository()
        {
            var persons = new List<Person>
            {
                new Person
                {
                    PersonId = 1,
                    Firstname = "Some X 2",
                    Surname = "XXXXX",
                    Gender = Gender.Male,
                    EmailAddress = "Email Address"
                },
                new Person
                {
                    PersonId = 2,
                    Firstname = "Some X 2",
                    Surname = "XXXXX",
                    Gender = Gender.Female,
                    EmailAddress = "Email Address"
                }
            };

            var mockPersonRepository = new Mock<IAsyncRepository<Person>>();
            mockPersonRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(persons);

            mockPersonRepository.Setup(repo => repo.AddAsync(It.IsAny<Person>())).ReturnsAsync(
                (Person person) =>
                {
                    persons.Add(person);
                    return person;
                });

            return mockPersonRepository;
        }
    }
}
