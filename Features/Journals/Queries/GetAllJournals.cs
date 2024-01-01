using AutoMapper;
using journal_service.ServiceManager;
using MediatR;

namespace journal_service.Features.Journals.Queries;

public class GetAllJournals
{
    public class GetAllJournalsQuery : IRequest<ICollection<JournalsResult>> { }

    public class JournalsResult
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string SocialSecurityNumber { get; set; } = string.Empty;
    }

    public class Handler : IRequestHandler<GetAllJournalsQuery, ICollection<JournalsResult>>
    {
        private readonly IServiceManager serviceManager;

        private readonly IMapper mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper) =>
            (this.serviceManager, this.mapper) = (serviceManager, mapper);

        public async Task<ICollection<JournalsResult>> Handle(GetAllJournalsQuery request, CancellationToken cancellationToken)
        {
            var journals = await serviceManager.Journal.GetAllJournalsAsync();

            var result = mapper.Map<ICollection<JournalsResult>>(journals);

            return result;
        }
    }
}
