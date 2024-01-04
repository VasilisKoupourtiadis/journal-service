using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Commands;

public class RemoveJournalEntry
{
    public class RemoveJournalEntryCommand : IRequest<Unit>
    {
        public Guid PatientId { get; set; }

        public Guid JournalEntryId { get; set; }
    }

    public class Handler : IRequestHandler<RemoveJournalEntryCommand, Unit>
    {
        private readonly IServiceManager serviceManager;

        public Handler(IServiceManager serviceManager) =>
            (this.serviceManager) = (serviceManager);

        public async Task<Unit> Handle(RemoveJournalEntryCommand request, CancellationToken cancellationToken)
        {
            var patient = await serviceManager.Patient.GetPatientAsync(request.PatientId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find patient");

            if (patient.Journal is null)
                throw new ArgumentNullException(nameof(patient.Journal), "Patient does not have a journal");

            var journalEntry = patient.Journal.GetJournalEntry(request.JournalEntryId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find journal entry");

            serviceManager.Journal.RemoveJournalEntry(journalEntry);

            await serviceManager.SaveAsync();

            return Unit.Value;
        }
    }
}
