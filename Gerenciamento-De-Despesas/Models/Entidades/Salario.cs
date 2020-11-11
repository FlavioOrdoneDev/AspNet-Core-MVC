namespace Gerenciamento_De_Despesas.Models.Entidades
{
    public class Salario
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int MesId { get; set; }
        public Mes Mes { get; set; }        
    }
}