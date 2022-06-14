using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaMultiAfetos.Context;
using ClinicaMultiAfetos.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace ClinicaMultiAfetos.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminPacienteController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPacienteController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPaciente
        //public async Task<IActionResult> Index()
        //{
        //      return View(await _context.Pacientes.ToListAsync());
        //}
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "NomeCompleto")
        {

            var resultado = _context.Pacientes.AsNoTracking()
                                            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.NomeCompleto.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "NomeCompleto");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminPaciente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Admin/AdminPaciente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPaciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,NomeCompleto,DataNascimento,Cid,Plano,QueixaInicial,Responsavel,PaiMae,Cpf,Rg,TelefoneContato,Endereco,EnderecoNumero,EnderecoCidade,EnderecoCep,EnderecoUf")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Admin/AdminPaciente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Admin/AdminPaciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteId,NomeCompleto,DataNascimento,Cid,Plano,QueixaInicial,Responsavel,PaiMae,Cpf,Rg,TelefoneContato,Endereco,EnderecoNumero,EnderecoCidade,EnderecoCep,EnderecoUf")] Paciente paciente)
        {
            if (id != paciente.PacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.PacienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Admin/AdminPaciente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Admin/AdminPaciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacientes == null)
            {
                return Problem("Entity set 'AppDbContext.Pacientes'  is null.");
            }
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return _context.Pacientes.Any(e => e.PacienteId == id);
        }
    }
}
