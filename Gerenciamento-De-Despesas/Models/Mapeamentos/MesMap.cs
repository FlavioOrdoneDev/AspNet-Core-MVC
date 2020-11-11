using System;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_De_Despesas.Models.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gerenciamento_De_Despesas.Models.Mapeamentos
{
    public class MesMap : IEntityTypeConfiguration<Mes>
    {
        public void Configure(EntityTypeBuilder<Mes> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();
            builder.Property(m => m.Nome).HasMaxLength(50).IsRequired();

            builder.HasMany(m => m.Despesas).WithOne(m => m.Mes).HasForeignKey(m => m.Id).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Salario).WithOne(m => m.Mes).HasForeignKey<Salario>(m => m.MesId).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Meses");
        }
    }
}
