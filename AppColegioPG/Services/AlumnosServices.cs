using AppColegioPG.Context;
using AppColegioPG.Models;
using AppColegioPG.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AppColegioPG.Services
{
    public class AlumnosServices : IMetodos<Alumnos>
    {
        private readonly DbSet<Alumnos> _dbSet;
        private readonly ColegioPGDbContext _context;

        public AlumnosServices(ColegioPGDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Alumnos>();
        }

        public async Task<Alumnos> Create(Alumnos entity)
        {
            Alumnos alumnos = new Alumnos()
            {
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono,
                Id_Cursos = entity.Id_Cursos,
                Correo = entity.Correo
            };

            EntityEntry<Alumnos> result = await _context.Alumnos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Alumnos>> GetAll()
        {
            return await _dbSet.Include(a => a.NavegacionCursos).ToListAsync();
        }

        public async Task<Alumnos> GetById(int id)
        {
            return await _context.Alumnos.
                Include(a => a.NavegacionCursos).
                FirstAsync(a => a.Id == id);
        }

        public async Task<Alumnos> Update(Alumnos entity)
        {
           var alumno = await GetById(entity.Id);
            if (alumno == null)
            {
                return entity;
            }

            alumno.Nombre = entity.Nombre;
            alumno.Apellido = entity.Apellido;
            alumno.Correo = entity.Correo;
            alumno.Telefono = entity.Telefono;
            alumno.Direccion = entity.Direccion;
            alumno.Id_Cursos = entity.Id_Cursos;

            await _context.SaveChangesAsync();
            return alumno;

        }
    }
}
