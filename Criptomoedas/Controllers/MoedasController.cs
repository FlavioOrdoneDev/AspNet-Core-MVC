using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Criptomoedas.Models;

namespace Criptomoedas.Controllers
{
    public class MoedasController : Controller
    {
        private readonly CriptomoedaContexto _context;

        public MoedasController(CriptomoedaContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Moedas.ToListAsync());
        }  
        
        public async Task<IActionResult> EscolhadeMoedas(List<Moeda> moedas)
        {
            foreach (var item in moedas)
            {
                if (item.CheckBoxMarcado == true)
                {
                    Moeda moeda = await _context.Moedas.FirstAsync(x => x.Id == item.Id);
                    moeda.Quantidade++;
                    _context.Update(moeda);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public JsonResult DadosGrafico()
        {
            return Json(_context.Moedas.Select(x => new { x.Nome, x.Quantidade }));
        }


        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Quantidade")] Moeda moeda)
        {
            if (ModelState.IsValid)
            {
                moeda.Quantidade = 0;
                _context.Add(moeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moeda);
        }        

        private bool MoedaExists(int id)
        {
            return _context.Moedas.Any(e => e.Id == id);
        }
    }
}
