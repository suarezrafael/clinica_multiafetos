using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using ClinicaMultiAfetos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Repositories
{
    public class PlanoPacienteRepository : IPlanoPacienteRepository
    {
        private readonly AppDbContext _context;

        public PlanoPacienteRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public IEnumerable<PlanoPaciente> Planos => _context.PlanosPaciente
            .Include(c => c.Paciente);


        public PlanoPaciente GetPlanoById(int planoId) => _context.PlanosPaciente
            .FirstOrDefault(l => l.PlanoPacienteId == planoId);
    }
}
