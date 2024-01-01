using journal_service.Domain;
using Microsoft.EntityFrameworkCore;

namespace journal_service.Features.Journals;

public class JournalService : IJournalService
{
    private readonly ApplicationContext context;

    public JournalService(ApplicationContext context) =>
        (this.context) = (context);

    public void AddJournal(Journal journal) =>
        context.Journals.Add(journal);

    public void RemoveJournal(Journal journal) =>
        context.Journals.Remove(journal);

    public async Task<ICollection<Journal>> GetAllJournalsAsync() =>
        await context.Journals.ToListAsync();

    public async Task<Journal> GetJournalAsync(Guid id) =>
        await context.Journals.FirstOrDefaultAsync(x => x.Id.Equals(id));
}
