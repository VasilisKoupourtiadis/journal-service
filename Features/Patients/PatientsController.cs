using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        var patients = await mediator.Send(new GetAllPatients.GetPatientsQuery());

        if (patients is null) 
            return NotFound(); 

        return Ok(patients);
    }
}
