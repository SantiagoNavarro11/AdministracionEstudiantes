using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    public class InscripcionMateria
    {
        /// <summary>
        /// Identificador único de la inscripción.
        /// </summary>
        public int IdInscripcion { get; set; }

        /// <summary>
        /// Identificador del usuario inscrito en la materia.
        /// </summary>
        public Usuario Usuarios { get; set; }

        /// <summary>
        /// Identificador de la materia en la que se inscribió el usuario.
        /// </summary>
        public Materia Materias { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="InscripcionMateria"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public InscripcionMateria()
        {
            IdInscripcion = 0;
            Usuarios = new Usuario();
            Materias = new Materia();
        }

        public InscripcionMateria(IDataReader lector)
        {
            IdInscripcion = lector.GetInt32(0);
            Usuarios = new Usuario();
            Usuarios.IdUsuario = lector.GetInt32(1);
            Materias = new Materia();
            Materias.IdMateria = lector.GetInt32(2);
        }
    }
}
