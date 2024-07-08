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
        public async Task<List<GetContactResponse>> GetAll()
        {
            return await _mediator.Send(new GetContactsQuery());
        }

        [HttpPost]
        public async Task<GetContactResponse> Post(CreateContactCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<List<GetContactResponse>> Delete([FromRoute]int id)
        {
            return await _mediator.Send(new DeleteContactCommand (id));
        }
        
        [HttpPut("{id}")]
        public async Task<GetContactResponse> Update([FromRoute]int id, UpdateContactCommand command)
        {
            command.Id = id;
            return await _mediator.Send(new UpdateContactCommand());
        }
    }
}
