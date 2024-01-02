using AutoMapper;
using journal_service.Domain;
using journal_service.Features.Journals.Commands;
using journal_service.Features.Journals.Queries;

namespace journal_service.Features.Journals;

public class JournalsMapper : Profile
{
    public JournalsMapper()
    {
        CreateMap<Journal, GetAllJournals.JournalsResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Patient.GetFullName()))
            .ForMember(dest => dest.SocialSecurityNumber, opt => opt.MapFrom(src => src.Patient.SocialSecurityNumber));
        
        CreateMap<Journal, GetJournal.JournalResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Patient.GetFullName()))
            .ForMember(dest => dest.SocialSecurityNumber, opt => opt.MapFrom(src => src.Patient.SocialSecurityNumber));

        CreateMap<Journal, AddJournal.JournalResult>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Patient.GetFullName()))
            .ForMember(dest => dest.SocialSecurityNumber, opt => opt.MapFrom(src => src.Patient.SocialSecurityNumber));

        CreateMap<JournalEntry, AddJournalEntry.JournalEntryResult>()
            .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.EntryDate.ToString("MMMM dd, yyyy")))
            .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Journal.PatientId));

    }
}
