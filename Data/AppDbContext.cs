using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Api.Models;
namespace SimuladoIronFit.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}

