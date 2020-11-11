using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciamento_De_Despesas.Models.Contexto
{
    public class DespesasContexto : DbContext
    {
        public DespesasContexto(DbContextOptions<DespesasContexto> opcoes)
            : base(opcoes) { }
    }
}
