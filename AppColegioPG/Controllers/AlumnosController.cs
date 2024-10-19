using AppColegioPG.DTOs;
using AppColegioPG.Models;
using AppColegioPG.Services;
using AppColegioPG.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppColegioPG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly IMetodos<Alumnos> _alumnosServices;
        private readonly IMapper _mapper;

        public AlumnosController(IMetodos<Alumnos> alumnosServices, IMapper mapper)
        {
            _alumnosServices = alumnosServices;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlumnosDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListaAlumnos()
        {
            try
            {
                var alumnos = await _alumnosServices.GetAll();
                var alumnosDTO = _mapper.Map<IEnumerable<AlumnosDTO>>(alumnos);
                return Ok(alumnosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la lista de alumnos.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlumnosDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAlumnoById(int id)
        {
            try
            {
                var alumno = await _alumnosServices.GetById(id);
                if (alumno == null)
                {
                    return NotFound($"Alumno con ID = {id} no encontrado.");
                }
                var alumnosDTO = _mapper.Map<AlumnosDTO>(alumno);
                return Ok(alumnosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el alumno.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AlumnosDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarAlumno([FromBody] AlumnosDTO alumnoDTO)
        {
            if (alumnoDTO == null)
            {
                return BadRequest("Datos del alumno inválidos.");
            }

            try
            {
                var alumno = _mapper.Map<Alumnos>(alumnoDTO);
                var result = await _alumnosServices.Create(alumno);
                var createdAlumnoDTO = _mapper.Map<AlumnosDTO>(result);
                return CreatedAtAction(nameof(GetAlumnoById), new { id = createdAlumnoDTO.Id }, createdAlumnoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar el alumno.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            try
            {
                var result = await _alumnosServices.Delete(id);
                if (!result)
                {
                    return NotFound($"Alumno con ID = {id} no encontrado.");
                }

                return StatusCode(StatusCodes.Status200OK, new {isSuccess=result });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el alumno.");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlumnosDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAlumno([FromBody] AlumnosDTO alumnoDTO)
        {
            if (alumnoDTO == null)
            {
                return BadRequest("Datos del alumno inválidos.");
            }

            try
            {
                var alumno = _mapper.Map<Alumnos>(alumnoDTO);
                var result = await _alumnosServices.Update(alumno);
                if (result == null)
                {
                    return NotFound($"Alumno con ID = {alumnoDTO.Id} no encontrado.");
                }
                var updatedAlumnoDTO = _mapper.Map<AlumnosDTO>(result);
                return Ok(updatedAlumnoDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el alumno.");
            }
        }
    }
}
