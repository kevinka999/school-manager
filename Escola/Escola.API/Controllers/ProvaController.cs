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
        public async Task<string> Criar([FromBody] ProvaDTO provaRequest)
        {
            if (provaRequest == null)
                return "Corpo da requisição invalido.";

            if (provaRequest.Gabarito == null || provaRequest.Gabarito.Count() == 0)
                return "O corpo devera conter informações de gabarito.";

            var mensagemRetorno = string.Empty;

            try
            {
                var prova = new Prova()
                {
                    Nome = provaRequest.Nome,
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

                mensagemRetorno = await _provaBo.CriarProva(prova, gabaritos);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao cadastrar prova.";
            }

            return mensagemRetorno;   
        }

        [HttpPost]
        [Route("Inativar")]
        public async Task<string> Inativar([FromBody] ProvaDTO provaRequest)
        {
            if (provaRequest == null || provaRequest.Nome == string.Empty)
                return "Corpo da requisição invalido.";

            var mensagemRetorno = string.Empty;

            try 
            {
                mensagemRetorno = await _provaBo.InativarProva(provaRequest.Nome);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao inativar prova.";
            }

            return mensagemRetorno;
        }
    }
}
