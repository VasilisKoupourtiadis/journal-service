using journal_service.Features.Patients;

namespace journal_service.ServiceManager;

public interface IServiceManager
{
    IPatientService Patient { get; }
}