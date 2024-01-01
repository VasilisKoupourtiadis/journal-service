using AutoMapper;
using journal_service.Domain;
using journal_service.Features.Patients.Commands;
using journal_service.Features.Patients.Queries;

namespace journal_service.Features.Patients;

public class PatientsMapper : Profile
{
    public PatientsMapper()
    {
        CreateMap<Patient, GetAllPatients.PatientResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

        CreateMap<Patient, AddPatient.PatientResult>();

        CreateMap<Patient, GetPatient.PatientResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

        CreateMap<Patient, UpdatePatient.UpdatePatientResult>();

        CreateMap<UpdatePatient.UpdatePatientResult, Patient>();
    }
}
