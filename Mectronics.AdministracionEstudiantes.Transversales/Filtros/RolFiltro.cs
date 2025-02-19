using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Filtros
{
    /// <summary>
    /// Representa los filtros aplicables a la búsqueda de roles en el sistema.
    /// </summary>
    public class RolFiltro
    {
        /// <summary>
        /// Identificador único del rol.
        /// </summary>
        public int IdRol { get; set; }

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="RolFiltro"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public RolFiltro()
        {
            IdRol = 0;
            Nombre = string.Empty;
        }
    }
}
