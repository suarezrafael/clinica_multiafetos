using ClinicaMultiAfetos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<DocumentoPaciente> DocumentosPaciente { get; set; }
        public DbSet<PlanoPaciente> PlanosPaciente { get; set; }
        public DbSet<ReciboPaciente> RecibosPaciente { get; set; }

        public DbSet<DocumentoClinica> DocumentosClinica { get; set; }

    }
}
