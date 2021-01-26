using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.BO.Interfaces
{
    public interface IAlunoBO
    {
        Task<string> CriarAluno(Aluno alunoRequest);
        Task<string> InativarAluno(string nomeAluno);
        Task<string> ResponderProva(string nomeAluno, string nomeProva, List<RespostaAluno> respostaRequest);
        Task<Aluno> MediaAluno(string nomeAluno);
        Task<List<Aluno>> Aprovados();
        Task<double> CalcularMediaTodasProva(int idAluno);
        Task<double> CalcularNotaProva(int idProva, int idAlunoProva);
        Task<List<AlunoProva>> ValidarProvaRespondida(int idAluno, int idProva);
    }
}
