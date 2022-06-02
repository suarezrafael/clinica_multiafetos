using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using ClinicaMultiAfetos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Repositories
{
    public class ReciboPacienteRepository : IReciboPacienteRepository
    {
        private readonly AppDbContext _context;

        public ReciboPacienteRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IEnumerable<ReciboPaciente> Recibos => _context.RecibosPaciente
            .Include(c => c.Paciente);

        public ReciboPaciente GetReciboById(int reciboId) => _context.RecibosPaciente
            .FirstOrDefault(l => l.ReciboPacienteId == reciboId);
    }
}
