﻿using journal_service.Domain;

namespace journal_service.Features.Patients.Service;

public interface IPatientService
{
    Task<ICollection<Patient>> GetAllPatientsAsync();

    Task<Patient> GetPatientAsync(Guid Id);

    void AddPatient(Patient patient);

    void RemovePatient(Patient patient);
}