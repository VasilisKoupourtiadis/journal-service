using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Patients;

public class GetPatientById
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
    }

    public class Handler : IRequestHandler<GetPatientQuery, PatientResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<PatientResult> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.Id);

            if (patient is null) throw new ArgumentNullException(nameof(patient), "Could not find patient");

            var result = mapper.Map<PatientResult>(patient);

            return result;
        }
    }
}
