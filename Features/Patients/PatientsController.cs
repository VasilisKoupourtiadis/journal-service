using MediatR;
using Microsoft.AspNetCore.Mvc;
using static journal_service.Features.Patients.GetAllPatients;
using static journal_service.Features.Patients.GetPatientById;
using static journal_service.Features.Patients.AddPatient;
using static journal_service.Features.Patients.RemovePatient;

namespace journal_service.Features.Patients;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IMediator mediator;

    public PatientsController(IMediator mediator) =>
    (this.mediator) = (mediator);


    [HttpGet]
    public async Task<ActionResult<ICollection<GetAllPatients.PatientResult>>> GetPatientsAsync()
    {
        var patients = await mediator.Send(new GetPatientsQuery());

        if (patients is null) 
            return NotFound(); 

        return Ok(patients);
    }

    [HttpGet("{id:guid}", Name = "GetPatientAsync")]
    public async Task<ActionResult<GetPatientById.PatientResult>> GetPatientAsync(Guid id)
    {
        var query = new GetPatientQuery
        {
            Id = id
        };

        var patient = await mediator.Send(query);

        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult> AddPatient([FromBody] AddPatientCommand command)
    {
        var patient = await mediator.Send(command);        

        return CreatedAtRoute("GetPatientAsync", new { id = patient.Id }, patient);
    }

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

}
