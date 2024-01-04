using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Commands;

public class UpdateJournalEntry
{
    public class UpdateJournalEntryCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public Guid JournalId { get; set; }

        public Guid PatientId { get; set; }

        public string EntryBy { get; set; } = string.Empty;

        public string Entry { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<UpdateJournalEntryCommand, Unit>
    {
        private readonly IServiceManager serviceManager;

        public Handler(IServiceManager serviceManager) =>
            (this.serviceManager) = (serviceManager);

        public async Task<Unit> Handle(UpdateJournalEntryCommand request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.PatientId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            if (patient.Journal is null)
                throw new ArgumentNullException(nameof(patient.Journal), "Patient does not have a journal");

            var journalEntry = patient.Journal.GetJournalEntry(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find journal entry");

            if (!journalEntry.JournalId.Equals(request.JournalId))
                throw new ArgumentException("Id mismatch: JournalId from request does not match source", nameof(request));

            journalEntry.UpdateJournalEntry(request.EntryBy, request.Entry, request.JournalId);

            await serviceManager.SaveAsync();

            return Unit.Value;
        }
    }
}
