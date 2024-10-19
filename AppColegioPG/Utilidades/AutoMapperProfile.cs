using AppColegioPG.DTOs;
using AppColegioPG.Models;
using AutoMapper;

namespace AppColegioPG.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            #region Cursos
            // primero colocar la tabla de modelo y luego del DTO
            CreateMap<Cursos, CursosDTO>().ReverseMap();
            #endregion

            #region Alumnos
            CreateMap<Alumnos, AlumnosDTO>()
                .ForMember(destino =>
                destino.NombreCurso,
                opt => opt.MapFrom(origen => origen.NavegacionCursos.Nombre)
                );

            CreateMap<AlumnosDTO, Alumnos>()
                .ForMember(destino =>
                  destino.NavegacionCursos,
                  opt => opt.Ignore()
                );
            #endregion
        }
    }
}
