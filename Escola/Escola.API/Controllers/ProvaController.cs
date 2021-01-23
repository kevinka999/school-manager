using Escola.API.DTO;
using Escola.BO.Interfaces;
using Escola.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.API.Controllers
{
    [Route("api/Prova")]
    [ApiController]
    public class ProvaController : ControllerBase
    {
        private readonly IProvaBO _provaBo;
        public ProvaController(IProvaBO provaBo) => _provaBo = provaBo;

        [HttpPost]
        [Route("Criar")]
        public async Task<string> Criar(ProvaDTO provaRequest)
        {
            if (provaRequest != null)
                return "Corpo da requisição invalido.";

            if (provaRequest.Gabarito.Count() == 0)
                return "A prova devera conter informações de gabarito.";

            var mensagemRetorno = string.Empty;

            try
            {
                var prova = new Prova()
                {
                    Nome = provaRequest.NomeProva,
                    QtdPerguntas = provaRequest.Gabarito.Count()
                };

                var gabaritos = new List<Gabarito>();

                foreach (var provaGabarito in provaRequest.Gabarito)
                {
                    gabaritos.Add(new Gabarito()
                    {
                        Pergunta = provaGabarito.Pergunta,
                        Resposta = provaGabarito.Resposta
                    });
                }

                await _provaBo.CriarProva(prova, gabaritos);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao cadastrar prova.";
            }
            finally
            {
                mensagemRetorno = "Prova cadastrada com sucesso!";
            }

            return mensagemRetorno;   
        }
        [HttpPut("{idProva}")]
        [Route("Inativar")]
        public async Task<string> Inativar(int idProva)
        {
            var mensagemRetorno = string.Empty;

            try 
            {
                await _provaBo.InativarProva(idProva);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao inativar prova.";
            }
            finally
            {
                mensagemRetorno = "Prova inativada com sucesso!";
            }

            return mensagemRetorno;
        }
    }
}
