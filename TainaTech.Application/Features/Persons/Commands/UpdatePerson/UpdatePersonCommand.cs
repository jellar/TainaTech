using MediatR;
using System;
using System.Text.Json.Serialization;
using TainaTech.Domain.Enums;

namespace TainaTech.Application.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest
    {
        public long PersonId { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
