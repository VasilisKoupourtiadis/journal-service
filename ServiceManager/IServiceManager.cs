using journal_service.Features.Journals.Service;
using journal_service.Features.Patients.Service;

namespace journal_service.ServiceManager;

public interface IServiceManager
{
    IPatientService Patient { get; }

    IJournalService Journal { get; }

    Task SaveAsync();
}