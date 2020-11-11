using System;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_De_Despesas.Models.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento_De_Despesas.Models.Mapeamentos
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedNever();
            builder.Property(d => d.Valor).IsRequired();

            builder.HasOne(d => d.Mes).WithMany(d => d.Despesas).HasForeignKey(d => d.MesId);

            builder.HasOne(d => d.TipoDespesa).WithMany(d => d.Despesas).HasForeignKey(d => d.TipoDespesaId);

            builder.ToTable("Despesas");
        }
    }
}
