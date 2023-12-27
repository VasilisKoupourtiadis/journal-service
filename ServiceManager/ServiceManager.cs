using journal_service.Features.Patients;

namespace journal_service.ServiceManager;

public class ServiceManager : IServiceManager
{
    private readonly ApplicationContext context;

    private IPatientService patientService;

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

    public Task SaveAsync() => context.SaveChangesAsync();
}
