using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IEventoLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Mectronics.AdministracionEstudiantes.MS.EventoLog.Controllers
{
    /// <summary>
    /// Controlador para la gestión de eventos de log.
    /// </summary>
    [Route("api/EventoLog")]
    [ApiController]
    public class EventoLogController : ControllerBase
    {
        private readonly IEventoLogServicio _eventoLogServicio;

        /// <summary>
        /// Constructor del controlador EventoLogController.
        /// </summary>
        /// <param name="eventoLogServicio">Servicio de eventos de log.</param>
        public EventoLogController(IEventoLogServicio eventoLogServicio)
        {
            _eventoLogServicio = eventoLogServicio;
        }

        /// <summary>
        /// Inserta un nuevo evento de log.
        /// </summary>
        /// <param name="eventoLogDto">Objeto con los datos del evento de log a insertar.</param>
        /// <returns>Devuelve un objeto con el estado de la operación y los datos insertados.</returns>
        /// <response code="200">Evento insertado exitosamente.</response>
        /// <response code="400">Error en la inserción del evento.</response>
        [HttpPost]
        [ProducesResponseType(typeof(RespuestaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespuestaDto), StatusCodes.Status400BadRequest)]
        public ActionResult<int> Insertar([FromBody] EventoLogDto eventoLogDto)
        {
            try
            {
                eventoLogDto = _eventoLogServicio.Insertar(eventoLogDto);

                return Ok(new RespuestaDto
                {
                    Exito = true,
                    Mensaje = "Evento de log insertado exitosamente.",
                    Datos = eventoLogDto
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
        /// Consulta una lista de eventos de log con filtros opcionales.
        /// </summary>
        /// <param name="filtro">Filtros de búsqueda para los eventos de log.</param>
        /// <returns>Lista de eventos de log.</returns>
        /// <response code="200">Registros consultados exitosamente.</response>
        /// <response code="404">No se encontraron registros.</response>
        /// <response code="400">Error en la consulta.</response>
        [HttpGet]
        [ProducesResponseType(typeof(RespuestaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespuestaDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RespuestaDto), StatusCodes.Status400BadRequest)]
        public ActionResult<List<EventoLogDto>> ConsultarLista([FromQuery] EventoLogFiltro filtro)
        {
            try
            {
                List<EventoLogDto> eventoLogs = _eventoLogServicio.ConsultarLista(filtro);

                if (eventoLogs == null || eventoLogs.Count == 0)
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
                    Datos = eventoLogs
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
