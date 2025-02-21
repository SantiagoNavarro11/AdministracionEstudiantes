using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IUsuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mectronics.AdministracionEstudiantes.MS.Usuario.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios.
    /// </summary>
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;

        /// <summary>
        /// Constructor del controlador de usuarios.
        /// </summary>
        /// <param name="usuarioServicio">Servicio de usuarios inyectado.</param>
        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        /// <summary>
        /// Inserta un nuevo usuario.
        /// </summary>
        /// <param name="usuarioDto">Datos del usuario a insertar.</param>
        /// <returns>Respuesta con el usuario creado.</returns>
        [HttpPost]
        public ActionResult<int> Insertar([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                usuarioDto = _usuarioServicio.Insertar(usuarioDto);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Usuario creado exitosamente.",
                    Datos = usuarioDto
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
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="usuarioDto">Datos del usuario a actualizar.</param>
        /// <returns>Respuesta con el usuario actualizado.</returns>
        [HttpPatch]
        public ActionResult<int> Actualizar([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                _usuarioServicio.Modificar(usuarioDto);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Usuario modificado exitosamente.",
                    Datos = usuarioDto
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
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <returns>Respuesta con el resultado de la eliminación.</returns>
        [HttpDelete("{id}")]
        public ActionResult<int> Eliminar(int id)
        {
            try
            {
                int resultado = _usuarioServicio.Eliminar(id);
                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = $"Usuario eliminado exitosamente. {resultado} registro eliminado.",
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
        /// Consulta un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a consultar.</param>
        /// <returns>Respuesta con el usuario consultado.</returns>
        [HttpGet("{id}")]
        public ActionResult<UsuarioDto> Consultar(int id)
        {
            try
            {
                UsuarioFiltro filtro = new UsuarioFiltro { IdUsuario = id };
                UsuarioDto usuarioDto = _usuarioServicio.Consultar(filtro);

                if (usuarioDto == null)
                {
                    return NotFound(new RespuestaDto
                    {
                        Exito = false,
                        Mensaje = "No se encontró el usuario solicitado.",
                        Datos = null
                    });
                }

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Registro consultado exitosamente.",
                    Datos = usuarioDto
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
        /// Consulta una lista de usuarios con filtros opcionales.
        /// </summary>
        /// <param name="filtro">Filtros de búsqueda de usuarios.</param>
        /// <returns>Lista de usuarios encontrados.</returns>
        [HttpGet]
        public ActionResult<List<UsuarioDto>> ConsultarLista([FromQuery] UsuarioFiltro filtro)
        {
            try
            {
                List<UsuarioDto> usuariosDto = _usuarioServicio.ConsultarListado(filtro);

                if (usuariosDto == null || usuariosDto.Count == 0)
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
                    Datos = usuariosDto
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
