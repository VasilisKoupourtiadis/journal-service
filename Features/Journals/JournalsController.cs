using MediatR;
using Microsoft.AspNetCore.Mvc;
using static journal_service.Features.Journals.Queries.GetAllJournals;
using static journal_service.Features.Journals.Commands.AddJournal;
using static journal_service.Features.Journals.Queries.GetJournal;
using static journal_service.Features.Journals.Commands.RemoveJournal;
using static journal_service.Features.Journals.Commands.AddJournalEntry;
using static journal_service.Features.Journals.Queries.GetJournalEntry;
using static journal_service.Features.Journals.Queries.GetAllJournalEntriesForPatient;
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

        var journal = await mediator.Send(query);

        return Ok(journal);
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

    [HttpGet]
    [Route("/api/[controller]/{patientId:guid}/entries")]
    public async Task<ActionResult<ICollection<JournalEntriesResult>>> GetJournalEntriesAsync(Guid patientId)
    {
        var query = new GetAllJournalEntriesForPatientQuery
        {
            PatientId = patientId
        };

        var journals = await mediator.Send(query);

        return Ok(journals);
    }

    [HttpGet]
    [Route("/api/[controller]/{patientId:guid}/entries/{journalEntryId}", Name = "GetJournalEntryAsync")]
    public async Task<ActionResult<GetJournalEntryQuery>> GetJournalEntryAsync(Guid patientId, Guid journalEntryId)
    {
        var query = new GetJournalEntryQuery
        {
            PatientId = patientId,
            JournalEntryId = journalEntryId
        };

        var journalEntry = await mediator.Send(query);

        return Ok(journalEntry);
    }

    [HttpPost]
    [Route("/api/[controller]/entry")]
    public async Task<ActionResult> AddJournalEntry([FromBody] AddJournalEntryCommand command)
    {
        var journalEntry = await mediator.Send(command);

        var routeValues = new
        {
            patientId = journalEntry.PatientId,
            journalEntryId = journalEntry.Id
        };

        return CreatedAtRoute("GetJournalEntryAsync", routeValues, journalEntry);
    }
}
