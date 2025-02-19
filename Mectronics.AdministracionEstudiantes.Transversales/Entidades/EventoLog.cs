using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    public class EventoLog
    {
        /// <summary>
        /// Identificador único del log de evento.
        /// </summary>
        public int IdLogs { get; set; }

        /// <summary>
        /// Tipo de evento registrado.
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Fecha en que ocurrió el evento.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Información adicional del evento.
        /// </summary>
        public string Informacion { get; set; }

        /// <summary>
        /// Identificador del usuario relacionado con el evento.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="EventoLog"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public EventoLog()
        {
            IdLogs = 0;
            Tipo = string.Empty;
            Fecha = DateTime.MinValue;
            Informacion = string.Empty;
            Usuario = new Usuario();
        }
    }
}
