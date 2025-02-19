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
        /// Inserta un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Entidad <see cref="Rol"/> que se desea insertar.</param>
        /// <returns>Número de filas afectadas en la base de datos.</returns>
        public int Insertar(Rol objEntidad)
        {
            string strComandoSql = "INSERT INTO Rol (Nombre) VALUES (@Nombre)";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@Nombre", objEntidad.Nombre, SqlDbType.VarChar);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el rol en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Modifica un rol existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Entidad <see cref="Rol"/> con los datos actualizados.</param>
        /// <returns>Número de filas afectadas en la base de datos.</returns>
        public int Modificar(Rol objEntidad)
        {
            string strComandoSql = "UPDATE Rol SET Nombre = @Nombre WHERE IdRol = @IdRol";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdRol", objEntidad.IdRol, SqlDbType.Int);
                _conexion.AgregarParametro("@Nombre", objEntidad.Nombre, SqlDbType.VarChar);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el rol en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Elimina un rol de la base de datos.
        /// </summary>
        /// <param name="IdRol">Identificador del rol a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public int Eliminar(int IdRol)
        {
            string strComandoSql = "DELETE FROM Rol WHERE IdRol = @IdRol";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdRol", IdRol, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el rol de la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Consulta un rol específico en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Filtro de búsqueda con el Id del rol.</param>
        /// <returns>Entidad <see cref="Rol"/> si se encuentra, null en caso contrario.</returns>
        public Rol Consultar(RolFiltro objEntidad)
        {
            Rol rol = null;
            string consultaSql = "SELECT IdRol, Nombre FROM Rol WHERE IdRol = @IdRol";

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
        public List<Rol> ConsultarListado(RolFiltro objEntidad)
        {
            List<Rol> roles = new List<Rol>();
            string consultaSql = "SELECT IdRol, Nombre FROM Rol";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AbrirConexion();
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
