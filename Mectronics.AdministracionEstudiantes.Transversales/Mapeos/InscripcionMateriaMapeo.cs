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
                    Nombres = lector.GetString(1)
                },
                Materia = new Materia
                {
                    Nombre = lector.GetString(2)

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
                        IdUsuario = lector.GetInt32(1),
                        Nombres = lector.GetString(2),
                        Apellidos = lector.GetString(3)
                    },
                    Materia = new Materia
                    {
                        IdMateria = lector.GetInt32(4),
                        Nombre = lector.GetString(5),
                        IdUsuarioProfesor = lector.GetInt32(6),
                        NombreProfesor = lector.GetString(7),
                        NumeroCreditos = lector.GetInt32(8)
                    }
                });
            }

            return inscripciones;
        }
    }
}


