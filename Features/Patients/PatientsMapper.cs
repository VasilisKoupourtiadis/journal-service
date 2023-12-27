using AutoMapper;
using journal_service.Domain;

namespace journal_service.Features.Patients;

public class PatientsMapper : Profile
{
    public PatientsMapper()
    {
        CreateMap<Patient, GetAllPatients.PatientResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

        CreateMap<Patient, AddPatient.PatientResult>();

        CreateMap<Patient, GetPatientById.PatientResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));
    }
}
