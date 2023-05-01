using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TP1examuml.Models;


namespace TP1examuml.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Medecin> Medecin { get; set; }
        public DbSet<Patient> Patient { get; set; }

        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<TypeConsultation> TypeConsultation { get; set;}
        public DbSet<Analyse> Analyse { get; set; }
        public DbSet<Laboratoire> Laboratoire { get; set;}
        public DbSet<Examen> Examen { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        
    }
}