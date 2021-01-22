using Escola.DAO.Interfaces;
using Escola.Dominio;
using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.DAO
{
    public class AlunoProvaDAO : IAlunoProvaDAO
    {
        private readonly DbContextConfiguration _context;
        public AlunoProvaDAO(DbContextConfiguration context) => _context = context;

        public async Task<AlunoProva> Adicionar(AlunoProva alunoProvaRequest)
        {
            var alunoProva = await _context.AlunoProva.AddAsync(alunoProvaRequest);
            await _context.SaveChangesAsync();

            return alunoProva.Entity;
        }

        public async Task<List<AlunoProva>> BuscarTodosPorId(int idAluno, int idProva) => await _context.AlunoProva
                                                                                                .Include(x => x.Aluno)
                                                                                                .Include(x => x.Prova)
                                                                                                .Where(x => x.AlunoId == idAluno && x.ProvaId == idProva)
                                                                                                .ToListAsync();
    }
}
