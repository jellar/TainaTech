using System.Collections.Generic;
using TainaTech.Domain.Entities;
using TainaTech.Domain.Enums;
using TainaTech.Persistance;

namespace TainaTech.APP.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(PeopleDbContext context)
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

            context.Persons.AddRange(persons);

            context.SaveChanges();
        }
    }
}
