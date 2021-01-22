using System.ComponentModel.DataAnnotations.Schema;

namespace Escola.Dominio.Entidades
{
    public class AlunoProva
    {
        public int Id { get; set; }

        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }

        [ForeignKey("Prova")]
        public int ProvaId { get; set; }

        public virtual Aluno Aluno { get; set; }
        public virtual Prova Prova { get; set; }
    }
}
