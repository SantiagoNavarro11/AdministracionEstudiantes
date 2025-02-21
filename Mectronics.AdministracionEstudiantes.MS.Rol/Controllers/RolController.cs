using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mectronics.AdministracionEstudiantes.MS.Rol.Controllers
{
    using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
    /// <summary>
    /// Controlador para la gestión de roles en el sistema.
    /// </summary>
    [Route("api/Rol")]
    [ApiController]
    public class RolController : ControllerBase
    {

        private readonly IRolServicio _rolServicio;

        /// <summary>
        /// Constructor del controlador de roles.
        /// </summary>
        /// <param name="rolServicio">Interfaz de servicio de roles.</param>
        public RolController(IRolServicio rolServicio)
        {
            _rolServicio = rolServicio;
        }

        /// <summary>
        /// Consulta una lista de roles basada en filtros.
        /// </summary>
        /// <param name="filtro">Parámetros de filtrado para la consulta de roles.</param>
        /// <returns>Lista de objetos RolDto.</returns>
        [HttpGet]
        public ActionResult<List<RolDto>> ConsultarLista([FromQuery] RolFiltro filtro)
        {
            try
            {
                List<RolDto> rolDto = _rolServicio.ConsultarLista(filtro);

                if (rolDto == null || rolDto.Count == 0)
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
                    Datos = rolDto
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

