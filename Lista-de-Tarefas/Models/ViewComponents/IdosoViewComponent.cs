using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista_de_Tarefas.Models.ViewComponents
{
    public class IdosoViewComponent : ViewComponent
    {
        private readonly TarefasContexto _contexto;

        public IdosoViewComponent(TarefasContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _contexto.Pessoas.Where(x => x.Idade >= 60).ToListAsync());
        }
    }
}
