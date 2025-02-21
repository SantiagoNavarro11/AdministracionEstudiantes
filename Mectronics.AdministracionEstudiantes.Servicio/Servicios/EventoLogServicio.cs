using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IEventoLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Servicio.Servicios
{
    public class EventoLogServicio : IEventoLogServicio
    {

        private readonly IEventoLogRepositorio _eventoLogRepositorio;
        private readonly IMapper _mapeo;

        public EventoLogServicio(IEventoLogRepositorio eventoLogRepositorio, IMapper mapeo)
        {
            _eventoLogRepositorio = eventoLogRepositorio;
            _mapeo = mapeo;
        }
        public List<EventoLogDto> ConsultarLista(EventoLogFiltro objFiltro)
        {
            if (objFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            List<EventoLog> eventoLog = _eventoLogRepositorio.ConsultarListado(objFiltro);
            List<EventoLogDto> tiendasDto = _mapeo.Map<List<EventoLogDto>>(eventoLog);

            return tiendasDto;
        }

        public EventoLogDto Insertar(EventoLogDto eventoLogDto)
        {
            EventoLog eventoLog = _mapeo.Map<EventoLog>(eventoLogDto);

            //ValidarDatos(eventoLog);

            eventoLogDto.IdLogs = _eventoLogRepositorio.Insertar(eventoLog);

            return eventoLogDto;
        }
    }
}
