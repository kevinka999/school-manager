using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.DAO.Interfaces
{
    public interface IRespostaAlunoDAO
    {
        Task<RespostaAluno> Adicionar(RespostaAluno respostaAlunoRequest);
        Task<List<RespostaAluno>> BuscarTodosPorId(int idAlunoProva);
    }
}
