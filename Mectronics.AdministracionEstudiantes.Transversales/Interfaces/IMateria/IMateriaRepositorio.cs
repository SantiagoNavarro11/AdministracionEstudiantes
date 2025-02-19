using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad <see cref="Materia"/>.
    /// </summary>
    public interface IMateriaRepositorio
    {
        /// <summary>
        /// Inserta una nueva materia en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Materia"/> con la información de la materia a insertar.</param>
        /// <returns>Retorna un entero indicando el resultado de la operación (generalmente el ID generado o filas afectadas).</returns>
        int Insertar(Materia objEntidad);

        /// <summary>
        /// Modifica la información de una materia existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Materia"/> con la información actualizada de la materia.</param>
        /// <returns>Retorna un entero indicando el resultado de la operación.</returns>
        int Modificar(Materia objEntidad);

        /// <summary>
        /// Elimina una materia de la base de datos mediante su identificador único.
        /// </summary>
        /// <param name="IdMateria">Identificador único de la materia a eliminar.</param>
        /// <returns>Retorna un entero indicando el resultado de la operación.</returns>
        int Eliminar(int IdMateria);

        /// <summary>
        /// Consulta una materia específica en la base de datos según los criterios definidos en <see cref="MateriaFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="MateriaFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna un objeto de tipo <see cref="Materia"/> con la información de la materia encontrada.</returns>
        Materia Consultar(MateriaFiltro objEntidad);

        /// <summary>
        /// Consulta un listado de materias en la base de datos según los criterios definidos en <see cref="MateriaFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="MateriaFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna una lista de objetos <see cref="Materia"/> con la información de las materias encontradas.</returns>
        List<Materia> ConsultarListado(MateriaFiltro objEntidad);
    }
}
