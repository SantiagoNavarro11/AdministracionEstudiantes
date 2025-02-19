using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    public class EventoLogMapeo
    {
        public static EventoLog Mapear(IDataReader lector)
        {
            // Verifica si el lector es nulo o si no contiene filas
            if (lector == null || !lector.Read())
                return null;

            // Retorna una nueva instancia de EventoLogMapeo con los valores obtenidos del lector
            return new EventoLog
            {
                IdLogs = lector.GetInt32(0),
                Tipo = lector.GetString(1),
                Fecha = lector.GetDateTime(2),
                Informacion = lector.GetString(3),
                Usuario = new Usuario
                {
                    IdUsuario = lector.GetInt32(4)
                }
            };
        }

        /// <summary>
        /// Convierte un <see cref="IDataReader"/> en una lista de objetos <see cref="EventoLogMapeo"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una lista de objetos <see cref="EventoLogMapeo"/> con los datos obtenidos.</returns>
        public static List<EventoLog> MapearLista(IDataReader lector)
        {
            var EventosLogsMapeos = new List<EventoLog>(); // Inicializa la lista donde se almacenarán los resultados

            // Verifica si el lector es nulo antes de proceder
            if (lector == null)
                return EventosLogsMapeos;

            // Itera a través de las filas del lector y agrega cada EventoLogMapeo a la lista
            while (lector.Read())
            {
                EventosLogsMapeos.Add(new EventoLog
                {
                    IdLogs = lector.GetInt32(0),
                    Tipo = lector.GetString(1),
                    Fecha = lector.GetDateTime(2),
                    Informacion = lector.GetString(3),
                    Usuario = new Usuario
                    {
                        IdUsuario = lector.GetInt32(4)
                    }
                });
            }

            return EventosLogsMapeos; // Retorna la lista con los objetos mapeados
        }
    }
}
