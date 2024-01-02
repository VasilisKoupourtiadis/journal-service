using AutoMapper;
using journal_service.Domain;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Commands;

public class AddJournalEntry
{
    public class AddJournalEntryCommand : IRequest<JournalEntryResult>
    {
        public Guid JournalId { get; set; }

        public string EntryBy { get; set; } = string.Empty;

        public string Entry { get; set; } = string.Empty;
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

    public class Handler : IRequestHandler<AddJournalEntryCommand, JournalEntryResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<JournalEntryResult> Handle(AddJournalEntryCommand request, CancellationToken cancellationToken)
        {
            var journal = await serviceManager.Journal.GetJournalAsync(request.JournalId)
                ?? throw new ArgumentNullException(nameof(request), "Could not find journal");

            var journalEntry = new JournalEntry(request.EntryBy, request.Entry, journal);            

            serviceManager.Journal.AddJournalEntry(journal, journalEntry);

            await serviceManager.SaveAsync();

            var result = mapper.Map<JournalEntryResult>(journalEntry);

            return result;
        }
    }
}
