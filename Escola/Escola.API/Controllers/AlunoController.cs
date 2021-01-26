using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Escola.API.DTO;
using Escola.BO.Interfaces;
using Escola.Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Escola.API.Controllers
{
    [Route("api/Aluno")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoBO _alunoBo;
        public AlunoController(IAlunoBO alunoBo)
        {
            _alunoBo = alunoBo;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<string> Criar([FromBody] AlunoDTO alunoRequest)
        {
            if (alunoRequest.Nome == string.Empty || alunoRequest == null)
                return "Corpo da requisição inválida.";

            var mensagemRetorno = string.Empty;

            try
            {
                mensagemRetorno = await _alunoBo.CriarAluno(new Aluno()
                {
                    Nome = alunoRequest.Nome
                });
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao cadastrar o aluno.";
            }

            return mensagemRetorno;
        }

        [HttpPost]
        [Route("Inativar")]
        public async Task<string> Inativar([FromBody] AlunoDTO alunoRequest)
        {
            if (alunoRequest.Nome == string.Empty || alunoRequest == null)
                return "Corpo da requisição inválida.";

            var mensagemRetorno = string.Empty;

            try
            {
                mensagemRetorno = await _alunoBo.InativarAluno(alunoRequest.Nome);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao inativar aluno.";
            }

            return mensagemRetorno;
        }

        [HttpPost]
        [Route("ResponderProva")]
        public async Task<string> ResponderProva([FromBody] RespostaAlunoDTO respostaAlunoRequest)
        {
            if (respostaAlunoRequest == null)
                return "Corpo da requisição invalido.";

            if (respostaAlunoRequest.Respostas == null || respostaAlunoRequest.Respostas.Count() == 0)
                return "O corpo devera conter informações de pergunta/resposta.";

            var mensagemRetorno = string.Empty;

            try
            {
                var respostaAluno = new List<RespostaAluno>();

                foreach (var resposta in respostaAlunoRequest.Respostas)
                {
                    respostaAluno.Add(new RespostaAluno()
                    {
                        Pergunta = resposta.Pergunta,
                        Resposta = resposta.Resposta
                    });
                }

                mensagemRetorno = await _alunoBo.ResponderProva(respostaAlunoRequest.NomeAluno, respostaAlunoRequest.NomeProva, respostaAluno);
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao responder a prova.";
            }

            return mensagemRetorno;
        }

        [HttpPost]
        [Route("Media")]
        public async Task<string> Media([FromBody] AlunoDTO alunoRequest)
        {
            if (alunoRequest.Nome == string.Empty || alunoRequest == null)
                return "Corpo da requisição invalido.";

            var mensagemRetorno = string.Empty;

            try
            {
                var aluno = await _alunoBo.MediaAluno(alunoRequest.Nome);

                if (aluno == null)
                    mensagemRetorno = "Aluno não encontrado ou inativado.";

                mensagemRetorno = JsonConvert.SerializeObject(new AlunoDTO() 
                {
                    Nome = aluno.Nome,
                    Media = aluno.Media
                });
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao buscar media do Aluno.";
            }

            return mensagemRetorno;
        }

        [HttpGet]
        [Route("Aprovados")]
        public async Task<string> Aprovados()
        {
            var mensagemRetorno = string.Empty;

            try 
            {
                var alunosAprovados = await _alunoBo.Aprovados();

                if (alunosAprovados != null && alunosAprovados.Count() > 0)
                {
                    var alunosRetornoDto = new List<AlunoDTO>();

                    foreach (var aluno in alunosAprovados)
                    {
                        alunosRetornoDto.Add(new AlunoDTO()
                        {
                            Nome = aluno.Nome,
                            Media = aluno.Media
                        });
                    }

                    mensagemRetorno = JsonConvert.SerializeObject(alunosRetornoDto);
                }
                else
                {
                    mensagemRetorno = "Nenhum aluno aprovado até o momento.";
                }
            }
            catch
            {
                mensagemRetorno = "Houve um problema ao verificar alunos aprovados.";
            }

            return mensagemRetorno;
        }
    }
}
