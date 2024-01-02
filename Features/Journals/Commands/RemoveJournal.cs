using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Commands;

public class RemoveJournal
{
    public class RemoveJournalCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<RemoveJournalCommand, Unit>
    {
        private readonly IServiceManager serviceManager;

        public Handler(IServiceManager serviceManager) =>
            (this.serviceManager) = (serviceManager);

        public async Task<Unit> Handle(RemoveJournalCommand request, CancellationToken cancellationToken)
        {
            var journal = await serviceManager.Journal.GetJournalAsync(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find journal");

            serviceManager.Journal.RemoveJournal(journal);

            await serviceManager.SaveAsync();

            return Unit.Value;
        }
    }
}
