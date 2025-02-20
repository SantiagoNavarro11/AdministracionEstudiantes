using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRol;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mectronics.AdministracionEstudiantes.Repositorio.Repositorios.RolRepositorio;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    /// <summary>
    /// Repositorio encargado de gestionar las operaciones sobre la entidad <see cref="Rol"/> en la base de datos.
    /// </summary>
    public class RolRepositorio : IRolRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        /// <summary>
        /// Constructor que inicializa una instancia de <see cref="RolRepositorio"/> con una conexión a la base de datos.
        /// </summary>
        /// <param name="conexion">Instancia de <see cref="IConexionBaseDatos"/> proporcionada por inyección de dependencias.</param>
        public RolRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }
        /// <summary>
        /// Consulta un rol específico en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Filtro de búsqueda con el Id del rol.</param>
        /// <returns>Entidad <see cref="Rol"/> si se encuentra, null en caso contrario.</returns>
        public Rol Consultar(RolFiltro objEntidad)
        {
            Rol rol = null;
            string consultaSql = "SELECT IdRol, NombreRol FROM Roles WHERE IdRol = @IdRol";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdRol", objEntidad.IdRol, SqlDbType.Int);
                _conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    rol = RolMapeo.Mapear(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar el rol en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return rol;
        }

        /// <summary>
        /// Consulta la lista completa de roles en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Filtro opcional para la búsqueda de roles.</param>
        /// <returns>Lista de entidades <see cref="Rol"/>.</returns>
        public List<Rol> ConsultarListado(RolFiltro filtro)
        {
            List<Rol> roles = new List<Rol>();
            string consultaSql = "SELECT r.IdRol, r.NombreRol FROM Roles r WHERE r.NombreRol LIKE @NombreRol";

            if (filtro.IdRol > 0)
            {
                consultaSql += " AND r.IdRol = @IdRol";
            }

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdRol", filtro.IdRol, SqlDbType.Int);
                _conexion.AgregarParametro("@NombreRol", $"%{filtro.Nombre}%", SqlDbType.VarChar);

                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    roles = RolMapeo.MapearLista(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la lista de roles en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return roles;
        }


    }
}
