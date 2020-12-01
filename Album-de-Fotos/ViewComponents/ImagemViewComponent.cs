using Album_de_Fotos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Album_de_Fotos.ViewComponents
{
    public class ImagemViewComponent : ViewComponent
    {
        private readonly Contexto _context;

        public ImagemViewComponent(Contexto context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _context.Imagens.Where(x => x.AlbumId == id).ToListAsync());
        }
    }
}
