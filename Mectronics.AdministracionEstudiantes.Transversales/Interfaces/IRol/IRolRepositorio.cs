using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRol
{
    /// <summary>
    /// Define las operaciones de acceso a datos para la entidad <see cref="Rol"/>.
    /// </summary>
    public interface IRolRepositorio
    {
        /// <summary>
        /// Inserta un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Rol"/> con la información del rol a insertar.</param>
        /// <returns>Retorna un entero que indica el resultado de la operación (puede ser el ID generado o filas afectadas).</returns>
        int InsertarRol(Rol objEntidad);

        /// <summary>
        /// Modifica la información de un rol existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Rol"/> con la información actualizada del rol.</param>
        /// <returns>Retorna un entero indicando el resultado de la operación.</returns>
        int ModificarRol(Rol objEntidad);

        /// <summary>
        /// Elimina un rol de la base de datos mediante su identificador único.
        /// </summary>
        /// <param name="IdRol">Identificador único del rol a eliminar.</param>
        /// <returns>Retorna un valor booleano indicando si la eliminación fue exitosa (true) o no (false).</returns>
        bool EliminarRol(int IdRol);

        /// <summary>
        /// Consulta un rol específico en la base de datos según los criterios definidos en <see cref="RolFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="RolFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna un objeto de tipo <see cref="Rol"/> con la información del rol encontrado.</returns>
        Rol ConsultarRol(RolFiltro objEntidad);

        /// <summary>
        /// Consulta un listado de roles en la base de datos según los criterios definidos en <see cref="RolFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="RolFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna una lista de objetos <see cref="Rol"/> con la información de los roles encontrados.</returns>
        List<Rol> ConsultarListado(RolFiltro objEntidad);
    }
}
