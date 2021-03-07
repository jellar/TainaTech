using System;
using System.Collections.Generic;
using System.Text;
using TainaTech.Application.Responses;

namespace TainaTech.Application.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandResponse :  BaseResponse
    {
        public CreatePersonCommandResponse() : base()
        {

        }
        public PersonDto Person { get; set; }
    }
}
