using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IEventoLog
{
    public interface IEventoLogServicio
    {
        EventoLogDto Insertar(EventoLogDto eventoLogDto);

        List<EventoLogDto> ConsultarLista(EventoLogFiltro eventoLogDto);
    }
}
