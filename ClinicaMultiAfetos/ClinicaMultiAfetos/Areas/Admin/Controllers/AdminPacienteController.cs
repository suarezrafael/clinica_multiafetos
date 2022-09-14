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
using ClinicaMultiAfetos.ViewModels;
using Microsoft.Extensions.Options;

namespace ClinicaMultiAfetos.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminPacienteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ConfigurationDocumentosClinica _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminPacienteController(AppDbContext context,
            IOptions<ConfigurationDocumentosClinica> myConfig,
            IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _myConfig = myConfig.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult PacienteDocumentos(int? id, string mensagem)
        {
            var paciente = _context.Pacientes
                         .Include(pd => pd.DocumentosPaciente)
                         .FirstOrDefault(p => p.PacienteId == id);

            if (paciente == null)
            {
                Response.StatusCode = 404;
                return View("PedidoNotFound", id.Value);
            }

            if (!string.IsNullOrEmpty(mensagem))
            {
                ViewBag.Mensagem = mensagem;
            }

            PacienteViewModel pacienteDocumentos = new PacienteViewModel()
            {
                Paciente = paciente,
                DocumentosPaciente = paciente.DocumentosPaciente
            };


            return View(pacienteDocumentos);
        }
        // POST: Admin/AdminPaciente
        public async Task<IActionResult> UploadFiles(int PacienteId, List<IFormFile> files)
        {
            string msg = "Arquivo enviado com sucesso.";

            if (files == null || files.Count == 0)
            {
                //ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";                
                //return View(ViewData);
                msg = "Error: Arquivo não selecionado.";
                return RedirectToAction(nameof(PacienteDocumentos), new { id = PacienteId, mensagem = msg });
            }

            if (files.Count > 1)
            {
                //ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                //return View(ViewData);
                msg = "Error: Selecione somente um arquivo.";
                return RedirectToAction(nameof(PacienteDocumentos), new { id = PacienteId, mensagem = msg });

            }

            long size = files.Sum(f => f.Length);
            var filePathsName = new List<string>();
            var filePathsNameWeb = new List<string>();
            var fileNames = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                   _myConfig.NomePastaDocumentosPaciente);

            var filePathWeb = Path.Combine(_myConfig.UrlSiteMultiAfetos,
                   _myConfig.NomePastaDocumentosPaciente);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") ||
                    formFile.FileName.Contains(".gif") ||
                    formFile.FileName.Contains(".png") ||
                    formFile.FileName.Contains(".pdf") ||
                    formFile.FileName.Contains(".docx") ||
                    formFile.FileName.Contains(".doc"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    var fileNameWithPathWeb = string.Concat(filePathWeb, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);
                    filePathsNameWeb.Add(fileNameWithPathWeb);

                    fileNames.Add(formFile.FileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // inserir registro na tabela
            DocumentoPaciente documentoPaciente = new DocumentoPaciente { 
                PacienteId = PacienteId,
                NomeArquivo = fileNames.First(),
                DocumentoUrl = filePathsNameWeb.First()
            };

            _context.Add(documentoPaciente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PacienteDocumentos), new { id = PacienteId, mensagem = msg });
        }

        public async Task<IActionResult> Deletefile(int documentoPacienteId, string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaDocumentosPaciente + "\\", fname);

            var pacienteId = 0;

            string msg = "";

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);

                if (_context.Pacientes == null)
                {
                    return Problem("Entity set 'AppDbContext.DocumentosPaciente'  is null.");
                }
                var documentoPaciente = await _context.DocumentosPaciente.FindAsync(documentoPacienteId);
                
                if (documentoPaciente != null)
                {
                    pacienteId = documentoPaciente.PacienteId;

                    _context.DocumentosPaciente.Remove(documentoPaciente);
                }

                await _context.SaveChangesAsync();

                msg = "Arquivo excluído.";
                
            }
            return RedirectToAction(nameof(PacienteDocumentos), new { id = pacienteId, mensagem = msg });
        }

        // GET: Admin/AdminPaciente
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "NomeCompleto")
        {

            var resultado = _context.Pacientes.AsNoTracking()
                                            .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado
                    .Where(p => p.NomeCompleto.Contains(filter) || 
                                p.PacienteId.ToString().Contains(filter) ||
                                p.Cpf.Contains(filter)||
                                p.TelefoneContato.Contains(filter)
                                
                                );
            }

            var model = await PagingList.CreateAsync(resultado, 4, pageindex, sort, "NomeCompleto");
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
