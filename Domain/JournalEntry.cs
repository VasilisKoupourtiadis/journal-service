﻿using System.ComponentModel.DataAnnotations;

namespace journal_service.Domain;

public class JournalEntry
{
    public JournalEntry(string entryBy, string entry) =>
        (EntryBy, Entry) = (entryBy, entry);

    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime EntryDate { get; private set; } = DateTime.Now;

    [MaxLength(50)]
    public string EntryBy { get; private set; } = string.Empty;

    [MaxLength(1000)]
    public string Entry { get; private set; } = string.Empty;
}