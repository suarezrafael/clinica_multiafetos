using ClinicaMultiAfetos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ClinicaMultiAfetos.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDocumentoClinicaController : Controller
    {

        private readonly ConfigurationDocumentosClinica _myConfig;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminDocumentoClinicaController(
            IOptions<ConfigurationDocumentosClinica> myConfig, 
            IWebHostEnvironment hostingEnvironment)
        {
            _myConfig = myConfig.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);
            var filePathsName = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                   _myConfig.NomePastaDocumentosClinica);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg") || 
                    formFile.FileName.Contains(".gif") ||
                    formFile.FileName.Contains(".png") ||
                    formFile.FileName.Contains(".pdf") ||
                    formFile.FileName.Contains(".docx")||
                    formFile.FileName.Contains(".doc"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                    filePathsName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;

            //retorna a viewdata
            return View(ViewData);
        }

        public IActionResult GetDocumentos()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
                 _myConfig.NomePastaDocumentosClinica);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);
            FileInfo[] files = dir.GetFiles();
            model.PathImagesDocto = _myConfig.NomePastaDocumentosClinica;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;
            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaDocumentosClinica + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);
                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} excluído com sucesso";
            }
            return View("index");
        }
    }
}
