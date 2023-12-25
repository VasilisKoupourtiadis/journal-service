namespace journal_service.Domain;

public class Journal
{
    public Journal(Patient patient) => (Patient) = (patient);

    public Journal() { }

    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid? PatientId { get; private set; }
    
    public Patient? Patient { get; private set; }

    public ICollection<JournalEntry> Entries { get; private set; } = new List<JournalEntry>();

    public void AddEntry(JournalEntry entry) => Entries.Add(entry);
}
