using journal_service.Domain;
using Microsoft.EntityFrameworkCore;

namespace journal_service.Features.Patients.Service;

public class PatientService : IPatientService
{
    private readonly ApplicationContext context;

    public PatientService(ApplicationContext context) =>
    this.context = context;

    public void AddPatient(Patient patient) =>
        context.Patients.Add(patient);

    public void RemovePatient(Patient patient) =>
        context.Patients.Remove(patient);

    public async Task<ICollection<Patient>> GetAllPatientsAsync() =>
        await context.Patients.Include(x => x.Journal.Entries).ToListAsync();

    public async Task<Patient> GetPatientAsync(Guid Id) =>
        await context.Patients.Include(x => x.Journal.Entries).FirstOrDefaultAsync(x => x.Id.Equals(Id));
}
