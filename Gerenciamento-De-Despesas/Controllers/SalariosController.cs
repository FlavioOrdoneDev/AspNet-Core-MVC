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
    public class SalariosController : Controller
    {
        private readonly DespesasContexto _context;

        public SalariosController(DespesasContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var despesasContexto = _context.Salarios.Include(s => s.Mes);
            return View(await despesasContexto.ToListAsync());
        }

        public IActionResult Create()
        {           

            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.Id != s.Salario.MesId), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor,MesId")] Salario salario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            

            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.Id != s.Salario.MesId), "Id", "Nome", salario.MesId);
            return View(salario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salario = await _context.Salarios.FindAsync(id);
            if (salario == null)
            {
                return NotFound();
            }

            salario.Mes = _context.Meses.Where(x => x.Id == salario.MesId).First();
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.Id != s.Salario.MesId), "Id", "Nome", salario.MesId);
            return View(salario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,MesId")] Salario salario)
        {
            if (id != salario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarioExists(salario.Id))
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
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.Id != s.Salario.MesId), "Id", "Nome", salario.MesId);
            return View(salario);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salario = await _context.Salarios.FindAsync(id);
            _context.Salarios.Remove(salario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarioExists(int id)
        {
            return _context.Salarios.Any(e => e.Id == id);
        }
    }
}
