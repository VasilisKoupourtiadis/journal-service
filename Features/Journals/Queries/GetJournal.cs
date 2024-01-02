using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Queries;

public class GetJournal
{
    public class GetJournalQuery : IRequest<JournalResult>
    {
        public Guid Id { get; set; }
    }

    public class JournalResult
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetJournalQuery, JournalResult>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<JournalResult> Handle(GetJournalQuery request, CancellationToken cancellationToken)
        {
            var journal = await serviceManager.Journal.GetJournalAsync(request.Id)
                ?? throw new ArgumentNullException(nameof(request), "Could not find journal");

            var result = mapper.Map<JournalResult>(journal);

            return result;
        }
    }
}
