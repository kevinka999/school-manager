using Escola.DAO.Interfaces;
using Escola.Dominio;
using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.DAO
{
    public class RespostaAlunoDAO : IRespostaAlunoDAO
    {
        private readonly DbContextConfiguration _context;
        public RespostaAlunoDAO(DbContextConfiguration context) => _context = context;

        public async Task<RespostaAluno> Adicionar(RespostaAluno respostaAlunoRequest)
        {
            var respostaAluno = await _context.RespostaAluno.AddAsync(respostaAlunoRequest);
            await _context.SaveChangesAsync();

            return respostaAluno.Entity;
        }

        public async Task<List<RespostaAluno>> BuscarTodosPorId(int idAlunoProva) => await _context.RespostaAluno
                                                                                            .Include(x => x.AlunoProva)
                                                                                            .Where(x => x.AlunoProvaId == idAlunoProva)
                                                                                            .ToListAsync();
    }
}
