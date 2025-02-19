using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    public class UsuarioMapeo
    {
        public static Usuario Mapear(IDataReader lector)
        {
            // Verifica si el lector es nulo o si no contiene filas
            if (lector == null || !lector.Read())
                return null;

            // Retorna una nueva instancia de Usuario con los valores obtenidos del lector
            return new Usuario
            {
                IdUsuario = lector.GetInt32(0),
                Nombres = lector.GetString(1),
                Apellidos = lector.GetString(2),
                Edad = lector.GetInt32(3),
                CorreoElectronico = lector.GetString(4),
                Contrasena = lector.GetString(5),
                Roles = new Rol
                {
                    IdRol = lector.GetInt32(6)
                },
                Fecha = lector.GetDateTime(7),
            };
        }

        /// <summary>
        /// Convierte un <see cref="IDataReader"/> en una lista de objetos <see cref="Usuario"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una lista de objetos <see cref="Usuario"/> con los datos obtenidos.</returns>
        public static List<Usuario> MapearLista(IDataReader lector)
        {
            var Usuarios = new List<Usuario>(); // Inicializa la lista donde se almacenarán los resultados

            // Verifica si el lector es nulo antes de proceder
            if (lector == null)
                return Usuarios;

            // Itera a través de las filas del lector y agrega cada Usuario a la lista
            while (lector.Read())
            {
                Usuarios.Add(new Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombres = lector.GetString(1),
                    Apellidos = lector.GetString(2),
                    Edad = lector.GetInt32(3),
                    CorreoElectronico = lector.GetString(4),
                    Contrasena = lector.GetString(5),
                    Roles = new Rol
                    {
                        IdRol = lector.GetInt32(6)
                    },
                    Fecha = lector.GetDateTime(7),
                });
            }

            return Usuarios; // Retorna la lista con los objetos mapeados
        }
    }
}
