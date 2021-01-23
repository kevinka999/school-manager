using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.BO.Interfaces
{
    public interface IProvaBO
    {
        Task CriarProva(Prova provaRequest, List<Gabarito> gabaritosRequest);
        Task InativarProva(int idProva);
    }
}
