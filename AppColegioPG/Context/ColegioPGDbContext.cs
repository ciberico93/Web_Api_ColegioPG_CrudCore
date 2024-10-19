using AppColegioPG.Models;
using Microsoft.EntityFrameworkCore;

namespace AppColegioPG.Context
{
    public class ColegioPGDbContext : DbContext
    {
        public ColegioPGDbContext(DbContextOptions<ColegioPGDbContext>options):base(options) {}

      public DbSet<Alumnos> Alumnos { get; set; }
      public DbSet<Cursos> Cursos { get; set; }
        
    }
}
