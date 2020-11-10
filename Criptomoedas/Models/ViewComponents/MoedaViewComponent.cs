using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Criptomoedas.Models.ViewComponents
{
    public class MoedaViewComponent : ViewComponent
    {
        private readonly CriptomoedaContexto _contexto;

        public MoedaViewComponent(CriptomoedaContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _contexto.Moedas.ToListAsync());
        }
    }
}
