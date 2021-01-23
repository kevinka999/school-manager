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

        public async Task CriarProva(Prova provaRequest, List<Gabarito> gabaritosRequest)
        {
            var prova = await _provaDao.Adicionar(provaRequest);

            foreach(var gabaritoRequest in gabaritosRequest)
            {
                gabaritoRequest.ProvaId = prova.Id;

                await _gabaritoDao.Adicionar(gabaritoRequest);
            }
        }

        public async Task InativarProva(int idProva)
        {
            var prova = await _provaDao.Buscar(idProva);
            prova.Ativo = false;

            await _provaDao.Atualizar(prova);
        }
    }
}
