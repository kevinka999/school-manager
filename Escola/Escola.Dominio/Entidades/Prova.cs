namespace Escola.Dominio.Entidades
{
    public class Prova
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int QtdPerguntas { get; set; }

        public bool Ativo { get; set; }
    }
}
