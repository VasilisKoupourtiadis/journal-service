using System.ComponentModel.DataAnnotations;

namespace journal_service.Domain;

public class Patient
{
    public Patient(string firstName, string lastName, string socialSecurityNumber, string? email, int? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string FirstName { get; private set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; private set; } = string.Empty;

    [RegularExpression(@"^(?!219-09-9999|078-05-1120)(?!666|000|9\\d{2})d{3}-(?!00)\\d{2}-(?!0{4})\\d{4}$")]
    public string SocialSecurityNumber { get; private set; } = string.Empty;

    [MaxLength(50)]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string? Email { get; private set; } = string.Empty;

    [Phone]
    [RegularExpression(@"^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$.", ErrorMessage = "Invalid phone number format")]
    public int? PhoneNumber { get; private set; }

    public Journal? Journal { get; private set; }

    public string GetFullName() => $"{LastName}, {FirstName}";

    public void RegisterJournal(Journal journal) => Journal = journal;
}
