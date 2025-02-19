using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    class Roles
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int IdRol { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string NombreRol { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Roles"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public Roles()
        {
            IdRol = 0;
            NombreRol = string.Empty;
        }

        public Roles(IDataReader lector)
        {
            IdRol = lector.GetInt32(0);
            NombreRol = lector.GetString(1);
        }
    }
}
