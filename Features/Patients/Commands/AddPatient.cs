using AutoMapper;
using journal_service.Domain;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Patients.Commands;

public class AddPatient
{
    public class AddPatientCommand : IRequest<PatientResult>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }
    }

    public class PatientResult
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }
    }

    public class Handler : IRequestHandler<AddPatientCommand, PatientResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<PatientResult> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient(
                request.FirstName,
                request.LastName,
                request.SocialSecurityNumber,
                request.Email,
                request.PhoneNumber);

            serviceManager.Patient.AddPatient(patient);

            await serviceManager.SaveAsync();

            var result = mapper.Map<PatientResult>(patient);

            return result;
        }
    }
}
