using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.DAO.Interfaces
{
    public interface IGabaritoDAO
    {
        Task<Gabarito> Adicionar(Gabarito gabaritoRequest);
        Task<List<Gabarito>> BuscarTodosPorId(int idProva);
    }
}
