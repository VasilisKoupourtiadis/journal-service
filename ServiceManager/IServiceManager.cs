using journal_service.Features.Journals;
using journal_service.Features.Patients;

namespace journal_service.ServiceManager;

public interface IServiceManager
{
    IPatientService Patient { get; }

    IJournalService Journal { get; }

    Task SaveAsync();
}