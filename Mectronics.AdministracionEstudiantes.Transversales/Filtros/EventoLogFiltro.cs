using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Filtros
{
    /// <summary>
    /// Representa los filtros aplicables a la búsqueda de registros de logs dentro del sistema.
    /// </summary>
    public class EventoLogFiltro
    {
        /// <summary>
        /// Identificador único del log registrado en el sistema.
        /// </summary>
        public string? TipoLog { get; set; }
        public DateTime? Fecha { get; set; }
        /// Constructor de la clase <see cref="EventoLogFiltro"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public EventoLogFiltro()
        {
            Fecha = DateTime.MinValue;
            TipoLog = string.Empty;
        }
    }
}
