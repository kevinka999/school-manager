using Escola.DAO.Interfaces;
using Escola.Dominio;
using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.DAO
{
    public class AlunoDAO : IAlunoDAO
    {
        private readonly DbContextConfiguration _context;
        public AlunoDAO(DbContextConfiguration context) => _context = context;

        public async Task<Aluno> Adicionar(Aluno alunoRequest)
        {
            var aluno = await _context.Aluno.AddAsync(alunoRequest);
            await _context.SaveChangesAsync();

            return aluno.Entity;
        }

        public async Task<Aluno> Atualizar(Aluno alunoRequest)
        {
            var aluno = _context.Aluno.Update(alunoRequest);
            await _context.SaveChangesAsync();

            return aluno.Entity;
        }

        public async Task<Aluno> Buscar(int idAluno) => await _context.Aluno.FirstOrDefaultAsync(x => x.Id == idAluno && x.Ativo);

        public async Task<List<Aluno>> BuscarTodos() => await _context.Aluno.Where(x => x.Ativo).ToListAsync();
    }
}
