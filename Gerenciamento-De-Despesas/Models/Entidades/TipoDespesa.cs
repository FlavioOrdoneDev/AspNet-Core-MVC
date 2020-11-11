using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciamento_De_Despesas.Models.Entidades
{
    public class TipoDespesa
    {
        public int Id { get; set; }

        [Remote("TipoDespesaExiste", "TipoDespesas")]
        public string Nome { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
    }
}
