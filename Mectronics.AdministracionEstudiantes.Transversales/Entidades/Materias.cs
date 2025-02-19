using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    class Materias
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Nombre de la materia.
        /// </summary>
        public string NombreMateria { get; set; }

        /// <summary>
        /// Número de créditos de la materia.
        /// </summary>
        public int NumeroCreditosMateria { get; set; }

        /// <summary>
        /// Identificador del profesor encargado de la materia.
        /// </summary>
        public int IdUsuarioProfesor { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Materias"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public Materias()
        {
            IdMateria = 0;
            NombreMateria = string.Empty;
            NumeroCreditosMateria = 0;
            IdUsuarioProfesor = 0;
        }

        public Materias(IDataReader lector)
        {
            IdMateria = lector.GetInt32(0);
            NombreMateria = lector.GetString(1);
            NumeroCreditosMateria = lector.GetInt32(2);
            IdUsuarioProfesor = lector.GetInt32(3);
        }
    }
}
