using Blue.Challenge.App.Extensions;
using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Queries;
using Blue.Challenge.Business.Responses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blue.Challenge.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<List<GetContactQueryResponse>> GetAll()
        {
            return await _mediator.Send(new GetContactsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<GetContactQueryResponse>> Post(CreateContactCommand command)
        {
            return this.ValidateResponse(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetContactQueryResponse>>> Delete([FromRoute]int id)
        {
            return this.ValidateResponse(await _mediator.Send(new DeleteContactCommand { Id = id }));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GetContactQueryResponse>> Update([FromRoute]int id, UpdateContactCommand command)
        {
            command.Id = id;
            return this.ValidateResponse(await _mediator.Send(new UpdateContactCommand()));
        }
    }
}
