using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Criptomoedas.Models
{
    public class CriptomoedaContexto : DbContext
    {
        public DbSet<Moeda> Moedas { get; set; }

        public CriptomoedaContexto(DbContextOptions<CriptomoedaContexto> opcoes) : base(opcoes) { }
    }
}
