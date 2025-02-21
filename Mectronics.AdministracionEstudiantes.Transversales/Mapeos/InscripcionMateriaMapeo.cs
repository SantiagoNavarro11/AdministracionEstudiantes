using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System.Data;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    public class InscripcionMateriaMapeo : Profile
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
                    Nombres = lector.GetString(1) // ✅ Mapear el nombre en lugar del ID
                },
                Materia = new Materia
                {
                    Nombre = lector.GetString(2) // ✅ Mapear el nombre de la materia en lugar del ID

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
            var inscripciones = new List<InscripcionMateria>();

            if (lector == null)
                return inscripciones;

            while (lector.Read())
            {
                inscripciones.Add(new InscripcionMateria
                {
                    IdInscripcion = lector.GetInt32(0),
                    Usuario = new Usuario
                    {
                        Nombres = lector.IsDBNull(1) ? string.Empty : lector.GetString(1)
                    },
                    Materia = new Materia
                    {
                        Nombre = lector.IsDBNull(2) ? string.Empty : lector.GetString(2)
                    }
                });
            }

            return inscripciones;
        }
    }
}


