using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IEventoLog
{
    public interface IEventoLogRepositorio
    {
        int Insertar(EventoLog objEntidad);
        List<EventoLog> ConsultarListado(EventoLogFiltro objEntidad);
    }
}
