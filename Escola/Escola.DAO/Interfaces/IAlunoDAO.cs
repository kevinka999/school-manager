using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.DAO.Interfaces
{
    public interface IAlunoDAO
    {
        Task<Aluno> Adicionar(Aluno alunoRequest);
        Task<Aluno> Atualizar(Aluno alunoRequest);
        Task<Aluno> Buscar(int idAluno);
        Task<Aluno> Buscar(string nomeAluno);
        Task<List<Aluno>> BuscarTodos();
    }
}
