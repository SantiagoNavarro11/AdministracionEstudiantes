using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IInscripcionMaterias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mectronics.AdministracionEstudiantes.MS.InscripcionMateria.Controllers
{
    /// <summary>
    /// Controlador para la gestión de inscripciones en materias.
    /// </summary>
    [Route("api/inscripcionMaterias")]
    [ApiController]
    public class InscripcionMateriaController : ControllerBase
    {
        private readonly IInscripcionMateriaServicio _InscripcionMateriasServicio;

        /// <summary>
        /// Constructor del controlador que inyecta el servicio de inscripciones.
        /// </summary>
        /// <param name="inscripcionMateriasServicio">Servicio de inscripciones.</param>
        public InscripcionMateriaController(IInscripcionMateriaServicio inscripcionMateriasServicio)
        {
            _InscripcionMateriasServicio = inscripcionMateriasServicio;
        }

        /// <summary>
        /// Inserta una nueva inscripción de materia.
        /// </summary>
        /// <param name="inscripcionMateriaDto">Datos de la inscripción.</param>
        /// <returns>Respuesta con la inscripción creada.</returns>
        [HttpPost]
        public ActionResult<int> Insertar([FromBody] InscripcionMateriaDto inscripcionMateriaDto)
        {
            try
            {
                inscripcionMateriaDto = _InscripcionMateriasServicio.Insertar(inscripcionMateriaDto);

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Inscripción de materia creada exitosamente.",
                    Datos = inscripcionMateriaDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDto
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Datos = null
                });
            }
        }

        /// <summary>
        /// Actualiza una inscripción de materia existente.
        /// </summary>
        /// <param name="inscripcionMateriaDto">Datos actualizados de la inscripción.</param>
        /// <returns>Respuesta con el resultado de la actualización.</returns>
        [HttpPatch]
        public ActionResult<int> Actualizar([FromBody] InscripcionMateriaDto inscripcionMateriaDto)
        {
            try
            {
                _InscripcionMateriasServicio.Modificar(inscripcionMateriaDto);

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Inscripción modificada exitosamente.",
                    Datos = inscripcionMateriaDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDto
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Datos = null
                });
            }
        }

        /// <summary>
        /// Elimina una inscripción de materia por ID.
        /// </summary>
        /// <param name="id">ID de la inscripción.</param>
        /// <returns>Respuesta con el resultado de la eliminación.</returns>
        [HttpDelete("{id}")]
        public ActionResult<int> Eliminar(int id)
        {
            try
            {
                int resultado = _InscripcionMateriasServicio.Eliminar(id);

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = $"Inscripción eliminada exitosamente. {resultado} registro eliminado.",
                    Datos = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDto
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Datos = null
                });
            }
        }

        /// <summary>
        /// Consulta una inscripción de materia por ID.
        /// </summary>
        /// <param name="id">ID de la inscripción.</param>
        /// <returns>Inscripción encontrada.</returns>
        [HttpGet("{id}")]
        public ActionResult<InscripcionMateriaDto> Consultar(int id)
        {
            try
            {
                InscripcionMateriaFiltro filtro = new InscripcionMateriaFiltro { IdInscripcion = id };
                InscripcionMateriaDto inscripcionMateriaDto = _InscripcionMateriasServicio.Consultar(filtro);

                if (inscripcionMateriaDto == null)
                {
                    return NotFound(new RespuestaDto
                    {
                        Exito = false,
                        Mensaje = "No se encontró la inscripción solicitada.",
                        Datos = null
                    });
                }

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Registro consultado exitosamente.",
                    Datos = inscripcionMateriaDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDto
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Datos = null
                });
            }
        }

        /// <summary>
        /// Consulta una lista de inscripciones de materias basadas en filtros.
        /// </summary>
        /// <param name="filtro">Criterios de búsqueda.</param>
        /// <returns>Lista de inscripciones encontradas.</returns>
        [HttpGet]
        public ActionResult<List<InscripcionMateriaDto>> ConsultarLista([FromQuery] InscripcionMateriaFiltro filtro)
        {
            try
            {
                List<InscripcionMateriaDto> inscripcionesDto = _InscripcionMateriasServicio.ConsultarListado(filtro);

                if (inscripcionesDto == null || inscripcionesDto.Count == 0)
                {
                    return NotFound(new RespuestaDto
                    {
                        Exito = false,
                        Mensaje = "No se encontró información.",
                        Datos = null
                    });
                }

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Registros consultados exitosamente.",
                    Datos = inscripcionesDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new RespuestaDto
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Datos = null
                });
            }
        }
    }
}
