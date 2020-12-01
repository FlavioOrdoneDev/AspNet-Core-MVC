using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Album_de_Fotos.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Album_de_Fotos.Controllers
{
    public class ImagemController : Controller
    {
        private readonly Contexto _context;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public ImagemController(Contexto context, IWebHostEnvironment webHostingEnvironment)
        {
            _context = context;
            _webHostingEnvironment = webHostingEnvironment;
        }

        public IActionResult Create(int id)
        {
            ViewBag.Destinos = _context.Albuns.FirstOrDefault(x => x.Id == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Link,AlbumId")] Imagem imagem, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_webHostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var filestream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(filestream);
                        imagem.Link = "~/Imagens/" + arquivo.FileName;
                    }
                }

                _context.Add(imagem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Album", new { id = imagem.AlbumId });
            }
            ViewData["Id"] = new SelectList(_context.Albuns, "Id", "Destino", imagem.AlbumId);
            return View(imagem);
        }
    }
}
