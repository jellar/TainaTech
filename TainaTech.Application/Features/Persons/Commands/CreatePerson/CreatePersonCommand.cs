using MediatR;
using System;
using TainaTech.Domain.Enums;

namespace TainaTech.Application.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommand : IRequest<CreatePersonCommandResponse>
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
