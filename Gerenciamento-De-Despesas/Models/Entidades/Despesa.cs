namespace Gerenciamento_De_Despesas.Models.Entidades
{
    public class Despesa
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int MesId { get; set; }
        public Mes Mes { get; set; }
        public int TipoDespesaId { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
    }
}