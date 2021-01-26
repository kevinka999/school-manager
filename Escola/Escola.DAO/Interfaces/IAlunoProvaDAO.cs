using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.DAO.Interfaces
{
    public interface IAlunoProvaDAO
    {
        Task<AlunoProva> Adicionar(AlunoProva alunoProvaRequest);
        Task<AlunoProva> Atualizar(AlunoProva alunoProvaRequest);
        Task<List<AlunoProva>> BuscarTodosPorId(int idAluno);
        Task<List<AlunoProva>> BuscarTodosPorId(int idAluno, int idProva);
    }
}
