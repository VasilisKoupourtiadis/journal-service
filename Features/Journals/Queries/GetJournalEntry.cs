using AutoMapper;
using journal_service.Domain;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Queries;

public class GetJournalEntry
{
    public class GetJournalEntryQuery : IRequest<JournalEntryResult>
    {
        public Guid PatientId { get; set; }

        public Guid JournalEntryId { get; set; }
    }

    public class JournalEntryResult
    {
        public Guid Id { get; set; }

        public Guid JournalId { get; set; }

        public Guid PatientId { get; set; }

        public string EntryDate { get; set; } = string.Empty;

        public string EntryBy { get; set; } = string.Empty;

        public string Entry { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetJournalEntryQuery, JournalEntryResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;
        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);
        
        public async Task<JournalEntryResult> Handle(GetJournalEntryQuery request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.PatientId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            if (patient.Journal is null)
                throw new ArgumentNullException(nameof(patient.Journal), "Patient does not have a journal");

            if (!patient.Journal.Entries.Any())
                throw new Exception("Patient journal has no entires");

            var journalEntry = patient.Journal.GetJournalEntry(request.JournalEntryId)
                ?? throw new ArgumentNullException(nameof(request), "Patient journal has no entries");

            var result = mapper.Map<JournalEntryResult>(journalEntry);

            return result;
        }
    }
}
