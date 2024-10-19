using AppColegioPG.Context;
using AppColegioPG.Models;
using AppColegioPG.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AppColegioPG.Services
{
    public class CursosServices : IMetodos<Cursos>
    {
        private readonly DbSet<Cursos> _dbSet;
        private readonly ColegioPGDbContext _context;

        public CursosServices(ColegioPGDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cursos>();
        }

        public async Task<Cursos> Create(Cursos entity)
        {
            Cursos cursos = new Cursos()
            {
                Nombre = entity.Nombre
            };

            EntityEntry<Cursos> result = await _dbSet.AddAsync(cursos);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            Cursos entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Cursos>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
                          
        public async Task<Cursos> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id_cursos == id);
        }

        public async Task<Cursos> Update(Cursos entity)
        {
            var curso = await GetById(entity.Id_cursos);

            if (curso == null)
            {
                return null;
            }

            curso.Nombre = entity.Nombre;
            
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
