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
        /// Consulta un listado de roles en la base de datos según los criterios definidos en <see cref="RolFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="RolFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna una lista de objetos <see cref="Rol"/> con la información de los roles encontrados.</returns>
        List<Rol> ConsultarListado(RolFiltro objEntidad);
    }
}
