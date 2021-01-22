using Escola.DAO.Interfaces;
using Escola.Dominio;
using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.DAO
{
    public class GabaritoDAO : IGabaritoDAO
    {
        private readonly DbContextConfiguration _context;
        public GabaritoDAO(DbContextConfiguration context) => _context = context;

        public async Task<Gabarito> Adicionar(Gabarito gabaritoRequest)
        {
            var gabarito = await _context.Gabarito.AddAsync(gabaritoRequest);
            await _context.SaveChangesAsync();

            return gabarito.Entity;
        }

        public async Task<List<Gabarito>> BuscarTodosPorId(int idProva) => await _context.Gabarito
                                                                                .Include(x => x.Prova)
                                                                                .Where(x => x.ProvaId == idProva)
                                                                                .ToListAsync();
    }
}
