using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    public static class MateriaMapeo
    {
        public static Materia Mapear(IDataReader lector)
        {
            // Verifica si el lector es nulo o si no contiene filas
            if (lector == null || !lector.Read())
                return null;

            // Retorna una nueva instancia de Materia con los valores obtenidos del lector
            return new Materia
            {
                IdMateria = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                NumeroCreditos = lector.GetInt32(2),
                IdUsuarioProfesor = lector.GetInt32(3),

            };
        }

        /// <summary>
        /// Convierte un <see cref="IDataReader"/> en una lista de objetos <see cref="Materia"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una lista de objetos <see cref="Materia"/> con los datos obtenidos.</returns>
        public static List<Materia> MapearLista(IDataReader lector)
        {
            var Materias = new List<Materia>(); // Inicializa la lista donde se almacenarán los resultados

            // Verifica si el lector es nulo antes de proceder
            if (lector == null)
                return Materias;

            // Itera a través de las filas del lector y agrega cada Materia a la lista
            while (lector.Read())
            {
                Materias.Add(new Materia
                {

                    IdMateria = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    NumeroCreditos = lector.GetInt32(2),
                    IdUsuarioProfesor = lector.GetInt32(3),
                });
            }

            return Materias; // Retorna la lista con los objetos mapeados
        }
    }

}

