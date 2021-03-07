using System;

namespace TainaTech.Application.Features.Persons.Queries.GetPerson
{
    public class PersonDetailsVm
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}