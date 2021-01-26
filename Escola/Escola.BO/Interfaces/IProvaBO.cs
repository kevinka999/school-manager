using Escola.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Escola.BO.Interfaces
{
    public interface IProvaBO
    {
        Task<string> CriarProva(Prova provaRequest, List<Gabarito> gabaritosRequest);
        Task<string> InativarProva(string nomeProva);
        Task<bool> ValidarRespostaProva(int idProva, List<RespostaAluno> respostaRequest);
        Task<Prova> BuscarProva(string nomeProva);
        Task<List<Prova>> BuscarTodasProva();
        Task<List<Gabarito>> BuscarGabaritos(int idProva);
    }
}
