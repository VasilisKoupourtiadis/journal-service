using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Queries;

public class GetAllJournalEntriesForPatient
{
    public class GetAllJournalEntriesForPatientQuery : IRequest<ICollection<JournalEntriesResult>>
    {
        public Guid PatientId { get; set; }
    }

    public class JournalEntriesResult
    {
        public Guid Id { get; set; }

        public Guid JournalId { get; set; }

        public Guid PatientId { get; set; }

        public string EntryDate { get; set; } = string.Empty;

        public string EntryBy { get; set; } = string.Empty;

        public string Entry { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetAllJournalEntriesForPatientQuery, ICollection<JournalEntriesResult>>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;
        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<ICollection<JournalEntriesResult>> Handle(GetAllJournalEntriesForPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.PatientId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            if (patient.Journal is null)
                throw new ArgumentNullException(nameof(patient.Journal), "Patient does not have a journal");

            var journalEntries = patient.Journal.Entries.OrderByDescending(x => x.EntryDate);

            var result = mapper.Map<ICollection<JournalEntriesResult>>(journalEntries);

            return result;
        }
    }
}
