using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Patients.Queries;

public class GetPatient
{
    public class GetPatientQuery : IRequest<PatientResult>
    {
        public Guid Id { get; set; }
    }

    public class PatientResult
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int? PhoneNumber { get; set; }
    }

    public class Handler : IRequestHandler<GetPatientQuery, PatientResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<PatientResult> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            var result = mapper.Map<PatientResult>(patient);

            return result;
        }
    }
}
