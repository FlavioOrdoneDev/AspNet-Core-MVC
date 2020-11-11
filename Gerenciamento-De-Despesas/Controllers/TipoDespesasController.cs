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
    public class TipoDespesasController : Controller
    {
        private readonly DespesasContexto _context;

        public TipoDespesasController(DespesasContexto context)
        {
            _context = context;
        }

        // GET: TipoDespesas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDespesas.ToListAsync());
        }

        // GET: TipoDespesas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDespesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoDespesa tipoDespesa)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = tipoDespesa.Nome + " foi cadastrado com sucesso.";

                _context.Add(tipoDespesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDespesa = await _context.TipoDespesas.FindAsync(id);
            if (tipoDespesa == null)
            {
                return NotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoDespesa tipoDespesa)
        {
            if (id != tipoDespesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = tipoDespesa.Nome + " foi atualizado com sucesso.";
                    _context.Update(tipoDespesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDespesaExists(tipoDespesa.Id))
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
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var tipoDespesa = await _context.TipoDespesas.FindAsync(id);
            TempData["Confirmacao"] = tipoDespesa.Nome + " foi excluído com sucesso.";
            _context.TipoDespesas.Remove(tipoDespesa);
            await _context.SaveChangesAsync();
            return Json(tipoDespesa.Nome + " excluído com sucesso.");
        }

        private bool TipoDespesaExists(int id)
        {
            return _context.TipoDespesas.Any(e => e.Id == id);
        }
    }
}
