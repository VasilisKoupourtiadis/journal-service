using journal_service.Features.Journals.Service;
using journal_service.Features.Patients;
using journal_service.Features.Patients.Service;

namespace journal_service.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly ApplicationContext context;

    private IPatientService patientService;

    private IJournalService journalService;

    public ServiceManager(ApplicationContext context) =>
    (this.context) = (context);

    public IPatientService Patient
    {
        get
        {
            var service = patientService is not null
                ? patientService
                : patientService = new PatientService(context);

            return service;
        }
    }

    public IJournalService Journal
    {
        get
        {
            var service = journalService is not null
                ? journalService
                : journalService = new JournalService(context);

            return service;
        }
    }

    public Task SaveAsync() => context.SaveChangesAsync();
}
