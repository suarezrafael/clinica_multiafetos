using ClinicaMultiAfetos.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

        DbSet<Paciente> Pacientes { get; set; }
        DbSet<Documento> Documentos { get; set; }
    }
}
