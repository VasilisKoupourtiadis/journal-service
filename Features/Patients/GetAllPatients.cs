using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Patients;

public class GetAllPatients
{
    public class GetPatientsQuery : IRequest<ICollection<PatientResult>> { }

    public class PatientResult
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int? PhoneNumber { get; set; }
    }

    public class Handler : IRequestHandler<GetPatientsQuery, ICollection<PatientResult>> 
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<ICollection<PatientResult>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await serviceManager.Patient.GetAllPatientsAsync();

            var results = mapper.Map<ICollection<PatientResult>>(patients);

            return results;
        }
    }
}