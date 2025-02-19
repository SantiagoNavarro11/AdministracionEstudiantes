using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    class InscripcionMaterias
    {
        /// <summary>
        /// Identificador único de la inscripción.
        /// </summary>
        public int IdInscripcion { get; set; }

        /// <summary>
        /// Identificador del usuario inscrito en la materia.
        /// </summary>
        public Usuarios Usuarios { get; set; }

        /// <summary>
        /// Identificador de la materia en la que se inscribió el usuario.
        /// </summary>
        public Materias Materias { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="InscripcionMaterias"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public InscripcionMaterias()
        {
            IdInscripcion = 0;
            Usuarios = new Usuarios();
            Materias = new Materias();
        }

        public InscripcionMaterias(IDataReader lector)
        {
            IdInscripcion = lector.GetInt32(0);
            Usuarios = new Usuarios();
            Usuarios.IdUsuario = lector.GetInt32(1);
            Materias = new Materias();
            Materias.IdMateria = lector.GetInt32(2);
        }
    }
}
