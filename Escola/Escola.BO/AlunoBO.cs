using Escola.BO.Interfaces;
using Escola.DAO.Interfaces;
using Escola.Dominio.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escola.BO
{
    public class AlunoBO : IAlunoBO
    {
        private readonly IAlunoDAO _alunoDao;
        private readonly IAlunoProvaDAO _alunoProvaDao;
        private readonly IRespostaAlunoDAO _respostaAlunoDao;
        private readonly IProvaBO _provaBo;
        public AlunoBO(IAlunoDAO alunoDao, IRespostaAlunoDAO respostaAlunoDao, IAlunoProvaDAO alunoProvaDao, IProvaBO provaBo)
        {
            _alunoDao = alunoDao;
            _alunoProvaDao = alunoProvaDao;
            _respostaAlunoDao = respostaAlunoDao;
            _provaBo = provaBo;
        }

        public async Task<string> CriarAluno(Aluno alunoRequest)
        {
            var alunoEncontrado = await _alunoDao.Buscar(alunoRequest.Nome);

            if (alunoEncontrado == null)
            {
                alunoRequest.Ativo = true;
                await _alunoDao.Adicionar(alunoRequest);

                return "Aluno cadastrado com sucesso!";
            }
            else
            {
                return "Aluno com nome " + alunoEncontrado.Nome + " já cadastrado no sistema.";
            }
        }

        public async Task<string> InativarAluno(string nomeAluno)
        {
            var aluno = await _alunoDao.Buscar(nomeAluno);

            if (aluno != null)
            {
                aluno.Ativo = false;

                await _alunoDao.Atualizar(aluno);

                return "Aluno inativado com sucesso!";
            }
            else
            {
                return "Aluno não encontrado ou inativado.";
            }
        }

        public async Task<string> ResponderProva(string nomeAluno, string nomeProva, List<RespostaAluno> respostaRequest)
        {
            var aluno = await _alunoDao.Buscar(nomeAluno);

            if (aluno == null)
                return "Aluno não encontrado ou inativado.";

            var prova = await _provaBo.BuscarProva(nomeProva);

            if (prova == null)
                return "Prova não encontrada ou inativada.";

            await ValidarProvaRespondida(aluno.Id, prova.Id);

            var alunoProva = await _alunoProvaDao.Adicionar(new AlunoProva()
            {
                AlunoId = aluno.Id,
                ProvaId = prova.Id,
                Ativo = true
            });

            if (await _provaBo.ValidarRespostaProva(prova.Id, respostaRequest))
            {
                foreach (var resposta in respostaRequest)
                {
                    await _respostaAlunoDao.Adicionar(new RespostaAluno()
                    {
                        AlunoProvaId = alunoProva.Id,
                        Pergunta = resposta.Pergunta,
                        Resposta = resposta.Resposta,
                    });
                }
            }

            aluno.Media = await CalcularMediaTodasProva(aluno.Id);

            await _alunoDao.Atualizar(aluno);

            return "Prova respondida com sucesso!";
        }

        public async Task<Aluno> MediaAluno(string nomeAluno) => await _alunoDao.Buscar(nomeAluno);

        public async Task<List<Aluno>> Aprovados()
        {
            var alunos = await _alunoDao.BuscarTodos();

            var alunosAprovados = new List<Aluno>();

            foreach (var aluno in alunos)
            {
                if (aluno.Media >= 7)
                    alunosAprovados.Add(aluno);
            }

            return alunosAprovados;
        }

        protected async Task<double> CalcularMediaTodasProva(int idAluno)
        {
            var alunoProvas = await _alunoProvaDao.BuscarTodosPorId(idAluno);
            var provasAtivas = await _provaBo.BuscarTodasProva();

            var somaTotalNotaProvas = 0.0;
            var quantidadeTotalProvas = provasAtivas.Count();

            foreach (var prova in provasAtivas)
            {
                var alunoProva = alunoProvas.FirstOrDefault(x => x.ProvaId == prova.Id);

                if (alunoProva == null)
                    continue;

                somaTotalNotaProvas += await CalcularNotaProva(prova.Id, alunoProva.Id);
            }

            var notaFinal = somaTotalNotaProvas / (double)quantidadeTotalProvas;

            return Math.Round(notaFinal, 2);
        }

        protected async Task<double> CalcularNotaProva(int idProva, int idAlunoProva)
        {
            var nota = 0.0;

            var gabaritos = await _provaBo.BuscarGabaritos(idProva);
            var respostas = await _respostaAlunoDao.BuscarTodosPorId(idAlunoProva);

            var quantidadePergunta = gabaritos.Count();
            var perguntasAcertadas = 0;

            foreach (var gabarito in gabaritos)
            {
                var resposta = respostas.FirstOrDefault(x => x.Pergunta == gabarito.Pergunta);

                if (resposta == null)
                    continue;

                if (resposta.Resposta == gabarito.Resposta)
                    perguntasAcertadas += 1;
            }

            nota = ((double)perguntasAcertadas / (double)quantidadePergunta) * 10;

            return Math.Round(nota, 2);
        }

        protected async Task ValidarProvaRespondida(int idAluno, int idProva)
        {
            var alunoProvas = await _alunoProvaDao.BuscarTodosPorId(idAluno, idProva);

            if (alunoProvas != null && alunoProvas.Count() > 0)
            {
                foreach (var prova in alunoProvas)
                {
                    prova.Ativo = false;
                    await _alunoProvaDao.Atualizar(prova);
                }
            }
        }
    }
}
