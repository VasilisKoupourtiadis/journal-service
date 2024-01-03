using AutoMapper;
using journal_service.Domain;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Commands;

public class AddJournal
{
    public class AddJournalCommand : IRequest<JournalResult>
    {
        public Guid PatientId { get; set; }
    }

    public class JournalResult
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<AddJournalCommand, JournalResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<JournalResult> Handle(AddJournalCommand request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.PatientId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            var journal = new Journal(patient);

            patient.RegisterJournal(journal);

            serviceManager.Journal.AddJournal(journal);            

            await serviceManager.SaveAsync();

            var result = mapper.Map<JournalResult>(journal);

            return result;
        }
    }
}
