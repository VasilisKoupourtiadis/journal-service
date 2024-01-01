using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Patients.Commands;

public class RemovePatient
{
    public class RemovePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<RemovePatientCommand, Unit>
    {
        private readonly IServiceManager serviceManager;

        public Handler(IServiceManager serviceManager) =>
            this.serviceManager = serviceManager;

        public async Task<Unit> Handle(RemovePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            serviceManager.Patient.RemovePatient(patient);

            await serviceManager.SaveAsync();

            return Unit.Value;
        }
    }
}
