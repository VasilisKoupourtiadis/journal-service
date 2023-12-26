using journal_service.Domain;
using Microsoft.EntityFrameworkCore;

namespace journal_service.Features.Patients;

public class PatientService : IPatientService
{
    private readonly ApplicationContext context;

    public PatientService(ApplicationContext context) =>
    (this.context) = (context);

    public void AddPatient(Patient patient) =>
        context.Patients.Add(patient);

    public void RemovePatient(Patient patient) =>
        context.Patients.Remove(patient);

    public async Task<ICollection<Patient>> GetAllPatientsAsync() =>
        await context.Patients.ToListAsync();

    public async Task<Patient> GetPatientAsync(string socialSecurityNumber) =>
        await context.Patients.FirstOrDefaultAsync(x => x.SocialSecurityNumber.Equals(socialSecurityNumber));
}
