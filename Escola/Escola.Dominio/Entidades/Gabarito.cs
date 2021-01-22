using System.ComponentModel.DataAnnotations.Schema;

namespace Escola.Dominio.Entidades
{
    public class Gabarito
    {
        public int Id { get; set; }

        [ForeignKey("Prova")]
        public int ProvaId { get; set; }

        public int Pergunta { get; set; }

        public string Resposta { get; set; }

        public virtual Prova Prova { get; set; }
    }
}
