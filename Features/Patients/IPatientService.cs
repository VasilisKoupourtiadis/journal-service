using journal_service.Domain;

namespace journal_service.Features.Patients;

public interface IPatientService
{
    Task<ICollection<Patient>> GetAllPatientsAsync();

    Task<Patient> GetPatientAsync(string socialSecurityNumber);

    void AddPatient(Patient patient);

    void RemovePatient(Patient patient);
}