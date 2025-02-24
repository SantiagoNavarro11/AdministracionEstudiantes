using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Filtros
{
    /// <summary>
    /// Representa los filtros aplicables a la búsqueda de usuarios en el sistema.
    /// </summary>
    public class UsuarioFiltro
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int IdRol { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="UsuarioFiltro"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public UsuarioFiltro()
        {
            IdUsuario = 0;
            Apellidos = string.Empty;
            CorreoElectronico = string.Empty;
            IdRol = 0;
        }
    }
}
