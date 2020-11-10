using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lista_de_Tarefas.Models
{
    public class TarefasContexto : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        public TarefasContexto(DbContextOptions<TarefasContexto> opcoes) : base(opcoes) { }
    }
}