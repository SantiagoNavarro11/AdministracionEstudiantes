using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    /// <summary>
    /// Clase estática para mapear datos de un <see cref="IDataReader"/> a objetos de tipo <see cref="Rol"/>.
    /// </summary>
    public static class RolMapeo
    {
        /// <summary>
        /// Mapea un <see cref="IDataReader"/> a una instancia de <see cref="Rol"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una instancia de <see cref="Rol"/> o <c>null</c> si no hay datos.</returns>
        public static Rol Mapear(IDataReader lector)
        {
            // Verifica si el lector es nulo o si no contiene filas
            if (lector == null || !lector.Read())
                return null;

            // Retorna una nueva instancia de Rol con los valores obtenidos del lector
            return new Rol
            {
                IdRol = lector.GetInt32(0), // Asigna el valor del primer campo como IdRol
                Nombre = lector.GetString(1), // Asigna el valor del segundo campo como Nombre
            };
        }

        /// <summary>
        /// Convierte un <see cref="IDataReader"/> en una lista de objetos <see cref="Rol"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una lista de objetos <see cref="Rol"/> con los datos obtenidos.</returns>
        public static List<Rol> MapearLista(IDataReader lector)
        {
            var Roles = new List<Rol>(); // Inicializa la lista donde se almacenarán los resultados

            // Verifica si el lector es nulo antes de proceder
            if (lector == null)
                return Roles;

            // Itera a través de las filas del lector y agrega cada rol a la lista
            while (lector.Read())
            {
                Roles.Add(new Rol
                {
                    IdRol = lector.GetInt32(0), // Asigna el valor del primer campo como IdRol
                    Nombre = lector.GetString(1), // Asigna el valor del segundo campo como Nombre
                });
            }

            return Roles; // Retorna la lista con los objetos mapeados
        }
    }
}

