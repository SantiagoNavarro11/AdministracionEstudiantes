using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorioInscripcionMaterias
{
    /// <summary>
    /// Interfaz que define las operaciones de acceso a datos para la entidad <see cref="InscripcionMateria"/>.
    /// Proporciona métodos para insertar, modificar, eliminar y consultar inscripciones de materias.
    /// </summary>
    public interface IInscripcionMateriaRepositorio
    {
        /// <summary>
        /// Inserta una nueva inscripción de materia en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="InscripcionMateria"/> con la información de la inscripción.</param>
        /// <returns>Retorna un entero indicando el número de filas afectadas en la operación.</returns>
        int Insertar(InscripcionMateria objEntidad);

        /// <summary>
        /// Modifica la información de una inscripción de materia existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="InscripcionMateria"/> con la información actualizada.</param>
        /// <returns>Retorna un entero indicando el número de filas afectadas en la operación.</returns>
        int Modificar(InscripcionMateria objEntidad);

        /// <summary>
        /// Elimina una inscripción de materia de la base de datos utilizando el identificador del usuario.
        /// </summary>
        /// <param name="IdUsuario">Identificador único del usuario cuya inscripción se eliminará.</param>
        /// <returns>Retorna un entero indicando el número de filas afectadas en la operación.</returns>
        int Eliminar(int IdUsuario);

        /// <summary>
        /// Consulta el listado completo de inscripciones de materias en la base de datos.
        /// </summary>
        /// <returns>Retorna una lista de objetos <see cref="InscripcionMateria"/> con las inscripciones encontradas.</returns>
        List<InscripcionMateria> ConsultarListado();
    }
}