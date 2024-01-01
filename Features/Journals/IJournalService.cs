﻿using journal_service.Domain;

namespace journal_service.Features.Journals;

public interface IJournalService
{
    void AddJournal(Journal journal);

    Task<ICollection<Journal>> GetAllJournalsAsync();

    Task<Journal> GetJournalAsync(Guid id);

    void RemoveJournal(Journal journal);
}