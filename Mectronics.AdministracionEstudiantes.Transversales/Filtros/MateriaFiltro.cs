using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Filtros
{
    /// <summary>
    /// Representa los filtros aplicables a la búsqueda de materias en el sistema.
    /// </summary>
    public class MateriaFiltro
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Nombre de la materia.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="MateriaFiltro"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public MateriaFiltro()
        {
            IdMateria = 0;
            Nombre = string.Empty;
        }
    }
}
