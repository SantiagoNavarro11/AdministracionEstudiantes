using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales
{
    public static class InscripcionMateriaMapeo
    {
        public static InscripcionMateria Mapear(IDataReader lector)
        {
            // Verifica si el lector es nulo o si no contiene filas
            if (lector == null || !lector.Read())
                return null;

            // Retorna una nueva instancia de InscripcionMateria con los valores obtenidos del lector
            return new InscripcionMateria
            {
                IdInscripcion = lector.GetInt32(0),
                Usuario = new Usuario
                {
                    IdUsuario = lector.GetInt32(1),
                },
                Materia = new Materia
                {
                    IdMateria = lector.GetInt32(2),
                }
            };
        }

        /// <summary>
        /// Convierte un <see cref="IDataReader"/> en una lista de objetos <see cref="InscripcionMateria"/>.
        /// </summary>
        /// <param name="lector">El <see cref="IDataReader"/> con los datos.</param>
        /// <returns>Una lista de objetos <see cref="InscripcionMateria"/> con los datos obtenidos.</returns>
        public static List<InscripcionMateria> MapearLista(IDataReader lector)
        {
            var InscripcionMaterias = new List<InscripcionMateria>(); // Inicializa la lista donde se almacenarán los resultados

            // Verifica si el lector es nulo antes de proceder
            if (lector == null)
                return InscripcionMaterias;

            // Itera a través de las filas del lector y agrega cada InscripcionMateria a la lista
            while (lector.Read())
            {
                InscripcionMaterias.Add(new InscripcionMateria
                {
                    IdInscripcion = lector.GetInt32(0),
                    Usuario = new Usuario
                    {
                        IdUsuario = lector.GetInt32(1),
                    },
                    Materia = new Materia
                    {
                        IdMateria = lector.GetInt32(2),
                    }
                });
            }

            return InscripcionMaterias; // Retorna la lista con los objetos mapeados
        }
    }
}


