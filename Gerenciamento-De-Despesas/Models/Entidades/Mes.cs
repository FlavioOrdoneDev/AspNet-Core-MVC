using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciamento_De_Despesas.Models.Entidades
{
    public class Mes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
        public Salario Salario { get; set; }
    }
}
