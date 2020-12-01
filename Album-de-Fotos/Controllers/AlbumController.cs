﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Album_de_Fotos.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Album_de_Fotos.Controllers
{
    public class AlbumController : Controller
    {
        private readonly Contexto _context;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        public AlbumController(Contexto context, IWebHostEnvironment webHostingEnvironment)
        {
            _context = context;
            _webHostingEnvironment = webHostingEnvironment;
        }

        // GET: Album
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albuns.ToListAsync());
        }

        // GET: Album/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Album/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Destino,FotoTopo,Inicio,Fim")] Album album, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                var linkUpload = Path.Combine(_webHostingEnvironment.WebRootPath, "Imagens");

                if (arquivo != null)
                {
                    using (var filestream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        await arquivo.CopyToAsync(filestream);
                        album.FotoTopo = "~/Imagens/" + arquivo.FileName;
                    }
                }

                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albuns.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            TempData["FotoTopo"] = album.FotoTopo;

            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Destino,FotoTopo,Inicio,Fim")] Album album, IFormFile arquivo)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (String.IsNullOrEmpty(album.FotoTopo))
                album.FotoTopo = TempData["FotoTopo"].ToString();

            if (ModelState.IsValid)
            {
                try
                {
                    var linkUpload = Path.Combine(_webHostingEnvironment.WebRootPath, "Imagens");

                    if (arquivo != null)
                    {
                        using (var filestream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                        {
                            await arquivo.CopyToAsync(filestream);
                            album.FotoTopo = "~/Imagens/" + arquivo.FileName;
                        }
                    }

                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(album);
        }

        

        // POST: Album/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _context.Albuns.FindAsync(id);
            _context.Albuns.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albuns.Any(e => e.Id == id);
        }
    }
}
