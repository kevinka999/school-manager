using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.DAO.Interfaces
{
    public interface IProvaDAO
    {
        Task<Prova> Adicionar(Prova provaRequest);
        Task<Prova> Atualizar(Prova provaRequest);
        Task<Prova> Buscar(int idProva);
        Task<Prova> Buscar(string nomeProva);
        Task<List<Prova>> BuscarTodos();
    }
}
