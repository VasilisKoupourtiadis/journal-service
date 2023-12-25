using journal_service.Domain;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Patient> Patients { get; private set; }

    public DbSet<Journal> Journals { get; private set; } 

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var patients = new List<Patient>()
        {
            new("John", "Stewart", "432-71-6221", "john.doe@gmail.com", 0829445668),
            new("Christopher", "River", "611-22-9012", "christopher.river@yahoo.com", 1804310031),
            new("Joaquin", "Matthews", "322-18-8711", "joaquin.matthews@outlook.com", 0955812204),
        };

        modelBuilder.Entity<Patient>().HasData(patients);
    }
}
