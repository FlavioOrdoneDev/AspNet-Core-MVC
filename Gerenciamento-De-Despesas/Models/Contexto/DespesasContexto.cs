using Gerenciamento_De_Despesas.Models.Entidades;
using Gerenciamento_De_Despesas.Models.Mapeamentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gerenciamento_De_Despesas.Models.Contexto
{
    public class DespesasContexto : DbContext
    {
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Salario> Salarios { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<TipoDespesa> TipoDespesas { get; set; }

        public DespesasContexto(DbContextOptions<DespesasContexto> opcoes)
            : base(opcoes) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoDespesaMap());
            modelBuilder.ApplyConfiguration(new DespesaMap());
            modelBuilder.ApplyConfiguration(new SalarioMap());
            modelBuilder.ApplyConfiguration(new MesMap());
        }
    }
}
