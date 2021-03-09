using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TainaTech.Application.Features.Persons.Commands.CreatePerson;
using TainaTech.Application.Features.Persons.Commands.UpdatePerson;
using TainaTech.Application.Features.Persons.Queries.GetPagedPersons;
using TainaTech.Application.Features.Persons.Queries.GetPerson;
using TainaTech.Application.Features.Persons.Queries.GetPersonsList;

namespace TainaTech.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<PersonListVm>>> Get()
        {
            var dtos = await _mediator.Send(new GetPersonListQuery());
            return Ok(dtos);
        }

        [HttpGet("getpagedpeople", Name = "GetPagedPersons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PagedPersonsVm>> GetPagedPersons(string firstname, int page = 1, int size = 10)
        {
            var getPagedPersonsQuery = new GetPagedPersonsQuery() { Firstname = firstname ,Page = page, Size = size };
            var dtos = await _mediator.Send(getPagedPersonsQuery);

            return Ok(dtos);    
        }

        [HttpGet("{id}", Name = "GetPersonById")]
        public async Task<ActionResult<PersonDetailsVm>> GetPersonById(int id)
        {
            var getEventDetailQuery = new GetPersonDetailsQuery() { PersonId = id };
            return Ok(await _mediator.Send(getEventDetailQuery));
        }

        [HttpPost(Name = "AddPerson")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePersonCommand createPersonCommand)
        {
            var id = await _mediator.Send(createPersonCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdatePersonCommand updatePersonCommand)
        {
            await _mediator.Send(updatePersonCommand);
            return NoContent();
        }
    }
}
