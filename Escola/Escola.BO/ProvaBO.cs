using Escola.BO.Interfaces;
using Escola.DAO.Interfaces;
using Escola.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Escola.BO
{
    public class ProvaBO : IProvaBO
    {
        private readonly IProvaDAO _provaDao;
        private readonly IGabaritoDAO _gabaritoDao;
        public ProvaBO(IProvaDAO provaDao, IGabaritoDAO gabaritoDao)
        {
            _provaDao = provaDao;
            _gabaritoDao = gabaritoDao;
        }

        public async Task<string> CriarProva(Prova provaRequest, List<Gabarito> gabaritosRequest)
        {
            var provaEncontrada = await _provaDao.Buscar(provaRequest.Nome);

            if (provaEncontrada == null)
            {
                provaRequest.Ativo = true;

                var prova = await _provaDao.Adicionar(provaRequest);

                foreach (var gabaritoRequest in gabaritosRequest)
                {
                    gabaritoRequest.ProvaId = prova.Id;

                    await _gabaritoDao.Adicionar(gabaritoRequest);
                }

                return "Prova cadastrada com sucesso!";
            }
            else
            {
                return "Prova com nome " + provaEncontrada.Nome + " já cadastrado no sistema.";
            } 
        }

        public async Task<string> InativarProva(string nomeProva)
        {
            var prova = await _provaDao.Buscar(nomeProva);

            if (prova != null)
            {
                prova.Ativo = false;

                await _provaDao.Atualizar(prova);

                return "Prova inativada com sucesso!";
            }
            else
            {
                return "Prova não encontrada ou inativada.";
            }
        }

        public async Task<bool> ValidarRespostaProva(int idProva, List<RespostaAluno> respostaRequest)
        {
            var gabaritosProva = await _gabaritoDao.BuscarTodosPorId(idProva);

            var respostaValida = true;

            foreach (var resposta in respostaRequest)
            {
                var achouPergunta = false;

                foreach (var gabarito in gabaritosProva)
                {
                    if (resposta.Pergunta == gabarito.Pergunta)
                    {
                        achouPergunta = true;
                        break;
                    }
                }
                
                if (!achouPergunta)
                {
                    respostaValida = false;
                    break;
                }
            }

            return respostaValida;
        }

        public async Task<Prova> BuscarProva(string nomeProva) => await _provaDao.Buscar(nomeProva);

        public async Task<List<Prova>> BuscarTodasProva() => await _provaDao.BuscarTodos();

        public async Task<List<Gabarito>> BuscarGabaritos(int idProva) => await _gabaritoDao.BuscarTodosPorId(idProva);
    }
}
