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
    public class AlunoTest
    {
        private IAlunoDAO _alunoDao;
        private IRespostaAlunoDAO _respostaAlunoDao;
        private IAlunoProvaDAO _alunoProvaDao;
        private IProvaBO _provaBo;
        private IAlunoBO _alunoBo;

        public AlunoTest()
        {
            _alunoDao = Substitute.For<IAlunoDAO>();
            _respostaAlunoDao = Substitute.For<IRespostaAlunoDAO>();
            _alunoProvaDao = Substitute.For<IAlunoProvaDAO>();
            _provaBo = Substitute.For<IProvaBO>();

            _alunoBo = new AlunoBO(_alunoDao, _respostaAlunoDao, _alunoProvaDao, _provaBo);
        }

        [Fact]
        public async Task Aluno_Cadastrado_Com_Sucesso()
        {
            // Arrange
            var alunoRequest = new Aluno() 
            {
                Nome = "Kevin Katzer"
            };

            // Act
            var alunoCriadoMensagem = await _alunoBo.CriarAluno(alunoRequest);

            // Assert
            Assert.NotNull(alunoCriadoMensagem);
            Assert.True(alunoCriadoMensagem == "Aluno cadastrado com sucesso!");
        }

        [Fact]
        public async Task Inativar_Aluno_Com_Sucesso()
        {
            // Arrange
            var alunoRequest = new Aluno()
            {
                Nome = "Kevin Katzer"
            };
            
            _alunoDao.Buscar(alunoRequest.Nome).Returns(Task.FromResult(new Aluno()));

            // Act
            var alunoInativadoMensagem = await _alunoBo.InativarAluno(alunoRequest.Nome);

            // Assert
            Assert.NotNull(alunoInativadoMensagem);
            Assert.True(alunoInativadoMensagem == "Aluno inativado com sucesso!");
        }

        [Fact]
        public async Task Validar_Todos_Alunos_Aprovados()
        {
            // Arrange
            var alunoAprovado = new Aluno() { Nome = "Kevin Katzer", Media = 8.0d };
            var alunoReprovado = new Aluno() { Nome = "Carl Sagan", Media = 4.0d }; // Isso que da escrever muito livro
            var todosAlunos = new List<Aluno>() { alunoAprovado, alunoReprovado };

            _alunoDao.BuscarTodos().Returns(Task.FromResult(todosAlunos));

            // Act
            var alunosAprovados = await _alunoBo.Aprovados();

            // Assert
            Assert.NotNull(alunosAprovados);
            Assert.IsType<List<Aluno>>(alunosAprovados);
            Assert.Contains(alunosAprovados, x => x.Nome == alunoAprovado.Nome);
        }

        [Fact]
        public async Task Calcular_Nota_De_Uma_Prova()
        {
            // Arrange
            var gabarito = new List<Gabarito>() { 
                new Gabarito() { Pergunta = 1, Resposta = "A" },
                new Gabarito() { Pergunta = 2, Resposta = "B" }
            };

            var resposta = new List<RespostaAluno>() 
            {
                new RespostaAluno() { Pergunta = 1, Resposta = "A" },
                new RespostaAluno() { Pergunta = 2, Resposta = "C" }
            };

            _provaBo.BuscarGabaritos(1).Returns(Task.FromResult(gabarito));
            _respostaAlunoDao.BuscarTodosPorId(1).Returns(Task.FromResult(resposta));

            // Act
            var notaAlunoRetorno = await _alunoBo.CalcularNotaProva(1, 1);

            // Assert
            Assert.True(notaAlunoRetorno == 5.0d);
        }

        [Fact]
        public async Task Validar_Se_Prova_Ja_Foi_Respondida()
        {
            // Arrange
            var alunoProvas = new List<AlunoProva>() { new AlunoProva() { AlunoId = 1, ProvaId = 1, Ativo = true } };

            _alunoProvaDao.BuscarTodosPorId(1, 1).Returns(Task.FromResult(alunoProvas));

            // Act
            var alunoProvasInativadas = await _alunoBo.ValidarProvaRespondida(1, 1);

            // Assert
            Assert.True(alunoProvasInativadas.Count > 0);
            Assert.Contains(alunoProvasInativadas, x => x.Ativo == false);
        }

    }
}
