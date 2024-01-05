using MediatR;
using Microsoft.AspNetCore.Mvc;
using static journal_service.Features.Journals.Queries.GetAllJournals;
using static journal_service.Features.Journals.Commands.AddJournal;
using static journal_service.Features.Journals.Queries.GetJournal;
using static journal_service.Features.Journals.Commands.RemoveJournal;
using static journal_service.Features.Journals.Commands.AddJournalEntry;
using static journal_service.Features.Journals.Queries.GetJournalEntry;
using static journal_service.Features.Journals.Queries.GetAllJournalEntriesForPatient;
using static journal_service.Features.Journals.Commands.RemoveJournalEntry;
using static journal_service.Features.Journals.Commands.UpdateJournalEntry;
using journal_service.Features.Journals.Queries;

namespace journal_service.Features.Journals;

[Route("api/[controller]")]
[ApiController]
public class JournalsController : ControllerBase
{
    private readonly IMediator mediator;

    public JournalsController(IMediator mediator) =>
        (this.mediator) = (mediator);

    /// <summary>
    /// Get all journals
    /// </summary>
    /// <returns>A collection of the journals</returns>
    /// <response code="200">Returns a collection of all the journals</response>
    [HttpGet]
    public async Task<ActionResult<ICollection<JournalsResult>>> GetJournals()
    {
        var journals = await mediator.Send(new GetAllJournalsQuery());

        if (journals is null)
            return NotFound();

        return Ok(journals);
    }

    /// <summary>
    /// Get a journal
    /// </summary>
    /// <returns>A journal</returns>
    /// <response code="200">Returns a journal</response>
    [HttpGet("{id:guid}", Name = "GetJournalAsync")]
    public async Task<ActionResult<GetJournal.JournalResult>> GetJournal(Guid id)
    {
        var query = new GetJournalQuery()
        {
            Id = id
        };

        var journal = await mediator.Send(query);

        return Ok(journal);
    }

    /// <summary>
    /// Add a journal
    /// </summary>
    /// <returns></returns>
    /// <response code="201">
    /// Returns an indicator that the resource has been created 
    /// and sets the location header for where the newly created resource can be found
    /// </response>
    [HttpPost]
    public async Task<ActionResult> AddJournal([FromBody] AddJournalCommand command)
    {
        var journal = await mediator.Send(command);

        return CreatedAtRoute("GetJournalAsync", new { id = journal.Id }, journal);
    }

    /// <summary>
    /// Delete a journal
    /// </summary>
    /// <returns></returns>
    /// <response code="204"></response>
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

    /// <summary>
    /// Get all journal entries for a patient
    /// </summary>
    /// <returns>A collection of all journal entries for a patient</returns>
    /// <response code="200">Returns a collection of all journal entries for a patient</response>
    [HttpGet]
    [Route("/api/[controller]/{patientId:guid}/entries")]
    public async Task<ActionResult<ICollection<JournalEntriesResult>>> GetJournalEntries(Guid patientId)
    {
        var query = new GetAllJournalEntriesForPatientQuery
        {
            PatientId = patientId
        };

        var journals = await mediator.Send(query);

        return Ok(journals);
    }

    /// <summary>
    /// Get a patient's journal entry
    /// </summary>
    /// <returns>A journal entry for a patient</returns>
    /// <response code="200">Returns a journal entry for a patient</response>
    [HttpGet]
    [Route("/api/[controller]/{patientId:guid}/entries/{journalEntryId}", Name = "GetJournalEntryAsync")]
    public async Task<ActionResult<GetJournalEntryQuery>> GetJournalEntry(Guid patientId, Guid journalEntryId)
    {
        var query = new GetJournalEntryQuery
        {
            PatientId = patientId,
            JournalEntryId = journalEntryId
        };

        var journalEntry = await mediator.Send(query);

        return Ok(journalEntry);
    }

    /// <summary>
    /// Add a journal entry to a patients journal
    /// </summary>
    /// <returns></returns>
    /// <response code="201">
    /// Returns an indicator that the resource has been created 
    /// and sets the location header for where the newly created resource can be found
    /// </response>
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

    /// <summary>
    /// Delete a patient's journal entry
    /// </summary>
    /// <returns></returns>
    /// <response code="204"></response>
    [HttpDelete]
    [Route("/api/[controller]/{patientId:guid}/entries/{journalEntryId}")]
    public async Task<ActionResult> RemoveJournalEntry(Guid patientId, Guid journalEntryId)
    {
        var command = new RemoveJournalEntryCommand
        { 
            PatientId = patientId, 
            JournalEntryId = journalEntryId 
        };

        await mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Update a patient's journal entry
    /// </summary>
    /// <returns></returns>
    /// <response code="204"></response>
    /// <response code="409">If an Id mismatch occurs</response>
    [HttpPut]
    [Route("/api/[controller]/{patientId:guid}/entries/{journalEntryId}")]
    public async Task<ActionResult> UpdateJournalEntry(Guid patientId, Guid journalEntryId, UpdateJournalEntryCommand command)
    {
        if (patientId != command.PatientId)
            return Conflict($"Id mismatch. Request path Id [${patientId}] and request body Id [${command.PatientId}] do not match");
        else if (journalEntryId != command.Id)
            return Conflict($"Id mismatch. Request path Id [${journalEntryId}] and request body Id [${command.Id}] do not match");

        await mediator.Send(command);

        return NoContent();
    }
}
