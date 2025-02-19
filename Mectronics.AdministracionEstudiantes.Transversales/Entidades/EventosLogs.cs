using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    class EventosLogs
    {
        /// <summary>
        /// Identificador único del log de evento.
        /// </summary>
        public int IdLogs { get; set; }

        /// <summary>
        /// Tipo de evento registrado.
        /// </summary>
        public string TipoLog { get; set; }

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
        public Usuarios Usuarios { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="EventosLogs"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public EventosLogs()
        {
            IdLogs = 0;
            TipoLog = string.Empty;
            Fecha = DateTime.MinValue;
            Informacion = string.Empty;
            Usuarios = new Usuarios();
        }

        public EventosLogs(IDataReader lector)
        {
            IdLogs = lector.GetInt32(0);
            TipoLog = lector.GetString(1);
            Fecha = lector.GetDateTime(2);
            Informacion = lector.GetString(3);
            Usuarios = new Usuarios();
            Usuarios.IdUsuario = lector.GetInt32(4);
        }
    }
}
