using MediatR;
using Microsoft.AspNetCore.Mvc;
using static journal_service.Features.Journals.Queries.GetAllJournals;
using static journal_service.Features.Journals.Commands.AddJournal;
using static journal_service.Features.Journals.Queries.GetJournal;
using static journal_service.Features.Journals.Commands.RemoveJournal;
using journal_service.Features.Journals.Queries;

namespace journal_service.Features.Journals;

[Route("api/[controller]")]
[ApiController]
public class JournalsController : ControllerBase
{
    private readonly IMediator mediator;

    public JournalsController(IMediator mediator) =>
        (this.mediator) = (mediator);

    [HttpGet]
    public async Task<ActionResult<ICollection<JournalsResult>>> GetJournalsAsync()
    {
        var journals = await mediator.Send(new GetAllJournalsQuery());

        if (journals is null) 
            return NotFound();

        return Ok(journals);
    }

    [HttpGet("{id:guid}", Name = "GetJournalAsync")]
    public async Task<ActionResult<GetJournal.JournalResult>> GetJournalAsync(Guid id)
    {
        var query = new GetJournalQuery()
        {
            Id = id
        };

        var result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddJournal([FromBody] AddJournalCommand command)
    {
        var journal = await mediator.Send(command);

        return CreatedAtRoute("GetJournalAsync", new { id = journal.Id }, journal);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemoveJournal(Guid id)
    {
        var command = new RemoveJournalCommand() 
        { 
            Id = id 
        };

        await mediator.Send(command);

        return NoContent();
    }
}
