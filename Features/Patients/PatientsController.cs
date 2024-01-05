using MediatR;
using Microsoft.AspNetCore.Mvc;
using static journal_service.Features.Patients.Queries.GetAllPatients;
using static journal_service.Features.Patients.Queries.GetPatient;
using static journal_service.Features.Patients.Commands.AddPatient;
using static journal_service.Features.Patients.Commands.RemovePatient;
using static journal_service.Features.Patients.Commands.UpdatePatient;
using journal_service.Features.Patients.Queries;

namespace journal_service.Features.Patients;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IMediator mediator;

    public PatientsController(IMediator mediator) =>
    (this.mediator) = (mediator);

    /// <summary>
    /// Get all patients
    /// </summary>
    /// <returns>A collection of all patients</returns>
    /// <response code="200">Returns a collection of all the patients</response>
    [HttpGet]
    public async Task<ActionResult<ICollection<GetAllPatients.PatientResult>>> GetPatients()
    {
        var patients = await mediator.Send(new GetPatientsQuery());

        return Ok(patients);
    }

    /// <summary>
    /// Get a patient
    /// </summary>
    /// <returns>A patient</returns>
    /// <response code="200">Returns a patient</response>
    [HttpGet("{id:guid}", Name = "GetPatientAsync")]
    public async Task<ActionResult<GetPatient.PatientResult>> GetPatient(Guid id)
    {
        var query = new GetPatientQuery
        {
            Id = id
        };

        var patient = await mediator.Send(query);

        return Ok(patient);
    }

    /// <summary>
    /// Add a patient
    /// </summary>
    /// <returns></returns>
    /// <response code="201">
    /// Returns an indicator that the resource has been created 
    /// and sets the location header for where the newly created resource can be found
    /// </response>
    [HttpPost]
    public async Task<ActionResult> AddPatient([FromBody] AddPatientCommand command)
    {
        var patient = await mediator.Send(command);        

        return CreatedAtRoute("GetPatientAsync", new { id = patient.Id }, patient);
    }

    /// <summary>
    /// Delete a patient
    /// </summary>
    /// <returns></returns>
    /// <response code="204"></response>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> RemovePatient(Guid id)
    {
        var command = new RemovePatientCommand 
        { 
            Id = id 
        };

        await mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Apply a patch doc with operation "replace" to the patient
    /// </summary>
    /// <returns></returns>
    /// <response code="204"></response>
    /// <response code="409">If an Id mismatch occurs</response>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdatePatient(Guid id, UpdatePatientCommand command)
    {
        if (id != command.Id) 
            return Conflict($"Id mismatch. Request path Id [${id}] and request body Id [${command.Id}] do not match");

        await mediator.Send(command);

        return NoContent();
    }

}
