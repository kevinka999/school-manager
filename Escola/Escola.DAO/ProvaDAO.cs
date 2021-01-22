using Escola.DAO.Interfaces;
using Escola.Dominio;
using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escola.DAO
{
    public class ProvaDAO : IProvaDAO
    {
        private readonly DbContextConfiguration _context;
        public ProvaDAO(DbContextConfiguration context) => _context = context;

        public async Task<Prova> Adicionar(Prova provaRequest)
        {
            var prova = await _context.Prova.AddAsync(provaRequest);
            await _context.SaveChangesAsync();

            return prova.Entity;
        }

        public async Task<Prova> Atualizar(Prova provaRequest)
        {
            var prova = _context.Prova.Update(provaRequest);
            await _context.SaveChangesAsync();

            return prova.Entity;
        }

        public async Task<Prova> Buscar(int idProva) => await _context.Prova.FirstOrDefaultAsync(x => x.Id == idProva && x.Ativo);

        public async Task<List<Prova>> BuscarTodos() => await _context.Prova.Where(x => x.Ativo).ToListAsync();
    }
}
