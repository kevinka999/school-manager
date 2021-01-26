using System;
using Xunit;
using Escola.DAO.Interfaces;
using Escola.BO.Interfaces;
using NSubstitute;
using Escola.BO;
using System.Threading.Tasks;
using Escola.Dominio.Entidades;
using System.Collections.Generic;

namespace Escola.Test
{
    public class ProvaTest
    {
        private readonly IProvaDAO _provaDao;
        private readonly IGabaritoDAO _gabaritoDao;
        private readonly IProvaBO _provaBo;
        public ProvaTest()
        {
            _provaDao = Substitute.For<IProvaDAO>();
            _gabaritoDao = Substitute.For<IGabaritoDAO>();
            _provaBo = new ProvaBO(_provaDao, _gabaritoDao);
        }

        [Fact]
        public async Task Prova_Cadastrada_Com_Sucesso()
        {
            // Arrange
            var prova = new Prova() { Nome = "Matematica" };
            var gabaritoProva = new List<Gabarito>() { new Gabarito() { Pergunta = 1, Resposta = "A" } };

            _provaDao.Adicionar(prova).Returns(Task.FromResult(new Prova { Id = 1, Nome = prova.Nome }));

            // Act
            var provaCriadaMensagemRetorno = await _provaBo.CriarProva(prova, gabaritoProva);

            // Assert
            Assert.NotNull(provaCriadaMensagemRetorno);
            Assert.True(provaCriadaMensagemRetorno == "Prova cadastrada com sucesso!");
        }

        [Fact]
        public async Task Inativar_Prova_Com_Sucesso()
        {
            // Arrange
            var prova = new Prova() { Nome = "Matematica" };

            _provaDao.Buscar(prova.Nome).Returns(Task.FromResult(prova));

            // Act
            var provaInativadaMensagemRetorno = await _provaBo.InativarProva(prova.Nome);

            // Assert
            Assert.NotNull(provaInativadaMensagemRetorno);
            Assert.True(provaInativadaMensagemRetorno == "Prova inativada com sucesso!");
        }
    }
}
