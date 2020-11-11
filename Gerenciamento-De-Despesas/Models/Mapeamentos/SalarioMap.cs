using System;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_De_Despesas.Models.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento_De_Despesas.Models.Mapeamentos
{
    public class SalarioMap : IEntityTypeConfiguration<Salario>
    {
        public void Configure(EntityTypeBuilder<Salario> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Valor).IsRequired();

            builder.HasOne(m => m.Mes).WithOne(m => m.Salario).HasForeignKey<Salario>(m => m.MesId);

            builder.ToTable("Salarios");
        }
    }
}
