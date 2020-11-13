using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_De_Despesas.Models.Contexto;
using Gerenciamento_De_Despesas.Models.Entidades;

namespace Gerenciamento_De_Despesas.Controllers
{
    public class DespesasController : Controller
    {
        private readonly DespesasContexto _context;

        public DespesasController(DespesasContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var despesasContexto = _context.Despesas.Include(d => d.Mes).Include(d => d.TipoDespesa);
            return View(await despesasContexto.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string procurar)
        {
            if (!String.IsNullOrEmpty(procurar))
                return View(await _context.Despesas.Where(x => x.TipoDespesa.Nome.ToUpper().Contains(procurar.ToUpper())).ToListAsync());

            return View(await _context.Despesas.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses, "Id", "Nome");
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Valor,MesId,TipoDespesaId")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "Despesa foi adicionada com sucesso.";
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "Id", "Nome", despesa.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "Id", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = _context.Despesas.Include(x => x.TipoDespesa).Include(x => x.Mes).ToList();

            var despesa = teste.Where(x => x.Id == id).FirstOrDefault();
            if (despesa == null)
            {
                return NotFound();
            }

            
            ViewData["Id"] = new SelectList(_context.Meses, "Id", "Nome", despesa.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "Id", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,MesId, TipoDespesaId")] Despesa despesa)
        {
            if (id != despesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = "Despesa atualizada com sucesso.";
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.Id))
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
            ViewData["Id"] = new SelectList(_context.Meses, "Id", "Nome", despesa.Id);
            ViewData["TipoDespesaId"] = new SelectList(_context.TipoDespesas, "Id", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var despesa = await _context.Despesas.Include(t => t.TipoDespesa).Where(x => x.Id == id).FirstOrDefaultAsync();
            TempData["Confirmacao"] = "Despesa foi excluída com sucesso.";
            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
            return Json( "Despesa excluída com sucesso.");
        }

        private bool DespesaExists(int id)
        {
            return _context.Despesas.Any(e => e.Id == id);
        }
    }
}
