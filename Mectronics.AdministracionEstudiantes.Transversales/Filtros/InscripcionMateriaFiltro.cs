using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Filtros
{
    /// <summary>
    /// Representa los filtros aplicables a la búsqueda de inscripciones en materias dentro del sistema.
    /// </summary>
    public class InscripcionMateriaFiltro
    {
        /// <summary>
        /// Identificador único de la inscripción.
        /// </summary>
        public int IdInscripcion { get; set; }

        /// <summary>
        /// Identificador único del profesor.
        /// </summary>
        public int IdProfesor { get; set; }

        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="InscripcionMateriaFiltro"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public InscripcionMateriaFiltro()
        {
            IdInscripcion = 0;
            IdMateria = 0;
            IdUsuario = 0;
            IdProfesor = 0;
        }
    }
}
