using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Dtos
{
    public class RolDto
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int IdRol { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string ?Nombre { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Rol"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
    }
}
