using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mectronics.AdministracionEstudiantes.MS.Materia.Controllers
{
    /// <summary>
    /// Controlador para la gestión de materias en la API.
    /// Proporciona operaciones para insertar, actualizar, eliminar y consultar materias.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        /// <summary>
        /// Servicio de materias para manejar la lógica de negocio.
        /// </summary>
        private readonly IMateriaServicio _materiaServicio;

        /// <summary>
        /// Inicializa una nueva instancia del <see cref="MateriaController"/> con el servicio inyectado.
        /// </summary>
        /// <param name="materiaServicio">Instancia del servicio de materias.</param>
        public MateriaController(IMateriaServicio materiaServicio)
        {
            _materiaServicio = materiaServicio;
        }

        /// <summary>
        /// Crea una nueva materia en el sistema.
        /// </summary>
        /// <param name="materiaDto">Objeto <see cref="MateriaDto"/> con la información de la materia a insertar.</param>
        /// <returns>Respuesta con la materia creada.</returns>
        [HttpPost]
        public ActionResult<int> Insertar([FromBody] MateriaDto materiaDto)
        {
            try
            {
                materiaDto = _materiaServicio.Insertar(materiaDto);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Materia creada exitosamente.",
                    Datos = materiaDto
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
        /// Actualiza una materia existente en el sistema.
        /// </summary>
        /// <param name="materiaDto">Objeto <see cref="MateriaDto"/> con la información actualizada.</param>
        /// <returns>Respuesta con la materia actualizada.</returns>
        [HttpPatch]
        public ActionResult<int> Actualizar([FromBody] MateriaDto materiaDto)
        {
            try
            {
                _materiaServicio.Actualizar(materiaDto);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Materia modificada exitosamente.",
                    Datos = materiaDto
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
        /// Elimina una materia del sistema por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la materia a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminación.</returns>
        [HttpDelete("{id}")]
        public ActionResult<int> Eliminar(int id)
        {
            try
            {
                int resultado = _materiaServicio.Eliminar(id);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = $"Materia eliminada exitosamente. {resultado} registro eliminado.",
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
        /// Consulta una materia específica por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la materia a consultar.</param>
        /// <returns>Objeto <see cref="MateriaDto"/> con la información de la materia consultada.</returns>
        [HttpGet("{id}")]
        public ActionResult<MateriaDto> Consultar(int id)
        {
            try
            {
                MateriaFiltro filtro = new MateriaFiltro { IdMateria = id };
                MateriaDto materiaDto = _materiaServicio.Consultar(filtro);

                if (materiaDto == null)
                {
                    return NotFound(new RespuestaDto
                    {
                        Exito = false,
                        Mensaje = "No se encontró la materia solicitada.",
                        Datos = null
                    });
                }
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Registro consultado exitosamente.",
                    Datos = materiaDto
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
        /// Consulta una lista de materias basadas en los filtros proporcionados.
        /// </summary>
        /// <param name="filtro">Objeto <see cref="MateriaFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Lista de materias encontradas.</returns>
        [HttpGet]
        public ActionResult<List<MateriaDto>> ConsultarLista([FromQuery] MateriaFiltro ?filtro)
        {
            try
            {
                List<MateriaDto> materiasDto = _materiaServicio.ConsultarLista(filtro);

                if (materiasDto == null || materiasDto.Count == 0)
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
                    Datos = materiasDto
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
