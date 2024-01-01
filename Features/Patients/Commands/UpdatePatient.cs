using AutoMapper;
using journal_service.ServiceManager;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace journal_service.Features.Patients.Commands;

public class UpdatePatient
{
    public class UpdatePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public JsonPatchDocument<UpdatePatientResult> PatchDoc { get; set; } = new JsonPatchDocument<UpdatePatientResult>();
    }

    public class UpdatePatientResult
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }
    }

    public class Handler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            var result = mapper.Map<UpdatePatientResult>(patient);

            request.PatchDoc.ApplyTo(result);

            mapper.Map(result, patient);

            await serviceManager.SaveAsync();

            return Unit.Value;
        }
    }
}
