using Escola.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Escola.Dominio
{
    public class DbContextConfiguration : DbContext
    {
        public DbContextConfiguration(DbContextOptions<DbContextConfiguration> options) : base(options) { }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<AlunoProva> AlunoProva { get; set; }
        public DbSet<Gabarito> Gabarito { get; set; }
        public DbSet<Prova> Prova { get; set; }
        public DbSet<RespostaAluno> RespostaAluno { get; set; }
    }
}
