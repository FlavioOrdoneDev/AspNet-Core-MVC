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

        // GET: Moedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moedas.ToListAsync());
        }

        // GET: Moedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        // GET: Moedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Quantidade")] Moeda moeda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moeda);
        }

        // GET: Moedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas.FindAsync(id);
            if (moeda == null)
            {
                return NotFound();
            }
            return View(moeda);
        }

        // POST: Moedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Quantidade")] Moeda moeda)
        {
            if (id != moeda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moeda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoedaExists(moeda.Id))
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
            return View(moeda);
        }

        // GET: Moedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        // POST: Moedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moeda = await _context.Moedas.FindAsync(id);
            _context.Moedas.Remove(moeda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoedaExists(int id)
        {
            return _context.Moedas.Any(e => e.Id == id);
        }
    }
}
