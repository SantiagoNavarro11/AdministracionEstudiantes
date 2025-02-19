using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    class Usuarios
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario.
        /// </summary>
        public Roles Roles { get; set; }

        /// <summary>
        /// Fecha de registro del usuario.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Usuarios"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public Usuarios()
        {
            IdUsuario = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            Edad = 0;
            CorreoElectronico = string.Empty;
            Contrasena = string.Empty;
            Roles = new Roles(); 
            Fecha = DateTime.MinValue;
        }

        public Usuarios(IDataReader lector)
        {
            IdUsuario = lector.GetInt32(0);
            Nombres = lector.GetString(1);
            Apellidos = lector.GetString(2);
            Edad = lector.GetInt32(3);
            CorreoElectronico = lector.GetString(4);
            Contrasena = lector.GetString(5);
            Roles = new Roles();
            Roles.IdRol = lector.GetInt32(6);
            Fecha = lector.GetDateTime(7);
        }
    }
}
