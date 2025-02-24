using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IUsuario
{
    public interface IUsuarioServicio
    {
        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Usuario"/> con la información del usuario a insertar.</param>
        /// <returns>Retorna un entero que indica el resultado de la operación (puede ser el ID generado o el número de filas afectadas).</returns>
        UsuarioDto Insertar(UsuarioDto objEntidad);

        /// <summary>
        /// Modifica la información de un usuario existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="Usuario"/> con la información actualizada del usuario.</param>
        /// <returns>Retorna un entero indicando el resultado de la operación.</returns>
        UsuarioDto Modificar(UsuarioDto objEntidad);

        /// <summary>
        /// Elimina un usuario de la base de datos mediante su identificador único.
        /// </summary>
        /// <param name="IdUsuario">Identificador único del usuario a eliminar.</param>
        /// <returns>Retorna un valor booleano indicando si la eliminación fue exitosa (true) o no (false).</returns>
        int Eliminar(int IdUsuario);

        /// <summary>
        /// Consulta un usuario específico en la base de datos según los criterios definidos en <see cref="UsuarioFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="UsuarioFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna un objeto de tipo <see cref="Usuario"/> con la información del usuario encontrado.</returns>
        UsuarioDto Consultar(UsuarioFiltro objEntidad);

        /// <summary>
        /// Consulta un listado de usuarios en la base de datos según los criterios definidos en <see cref="UsuarioFiltro"/>.
        /// </summary>
        /// <param name="objEntidad">Objeto de tipo <see cref="UsuarioFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Retorna una lista de objetos <see cref="Usuario"/> con la información de los usuarios encontrados.</returns>
        List<UsuarioDto> ConsultarListado(UsuarioFiltro objEntidad);

        UsuarioDto Autenticar(string correo, string contrasena);
    }
}
