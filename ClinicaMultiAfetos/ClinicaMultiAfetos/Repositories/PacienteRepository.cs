using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using ClinicaMultiAfetos.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaMultiAfetos.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IEnumerable<Paciente> Pacientes => _context.Pacientes
                                                            .Include(d => d.DocumentosPaciente)
                                                            .Include(r => r.RecibosPaciente)
                                                            .Include(p => p.PlanosPaciente);

        public Paciente GetPacienteByCpf(string cpf) => _context.Pacientes.
                                   FirstOrDefault(l => l.Cpf.Equals(cpf));

        public Paciente GetPacienteById(int pacienteId) => _context.Pacientes.
                                   FirstOrDefault(l => l.PacienteId.Equals(pacienteId));

        public Paciente GetPacienteByRg(string rg) => _context.Pacientes.
                                   FirstOrDefault(l => l.Rg.Equals(rg));

    }
}
