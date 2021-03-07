using System;
using TainaTech.Domain.Enums;

namespace TainaTech.Application.Features.Persons.Commands.CreatePerson
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}