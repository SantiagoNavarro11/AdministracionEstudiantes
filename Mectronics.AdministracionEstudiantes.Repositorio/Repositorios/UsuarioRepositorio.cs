using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IUsuario;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        /// <summary>
        /// Constructor que inicializa una instancia de <see cref="UsuarioRepositorio"/> con una conexión a la base de datos.
        /// </summary>
        /// <param name="conexion">Instancia de <see cref="IConexionBaseDatos"/> proporcionada por inyección de dependencias.</param>
        public UsuarioRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }

        public Usuario Autenticar(string correo)
        {
            Usuario usuario = null;
            string consultaSql = "SELECT U.IdUsuario, U.Nombres, U.Apellidos, U.Edad, U.CorreoElectronico, U.Contrasena, R.IdRol, R.NombreRol AS Rol, U.Fecha " +
                "FROM Usuarios U INNER JOIN Roles R ON U.IdRoles = R.IdRol WHERE U.CorreoElectronico = @CorreoElectronico";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@CorreoElectronico", correo, SqlDbType.VarChar);

                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    usuario = UsuarioMapeo.Mapear(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar el usuario en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return usuario;
        }
        public int Insertar(Usuario objEntidad)
        {
            string strComandoSql = @"INSERT INTO Usuarios (Nombres, Apellidos, Edad, CorreoElectronico, Contrasena, IdRoles, Fecha) 
                     VALUES (@Nombres, @Apellidos, @Edad, @CorreoElectronico, @Contrasena, @IdRoles, @Fecha) SELECT SCOPE_IDENTITY();";
            int IdUsuario = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@Nombres", objEntidad.Nombres, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Apellidos", objEntidad.Apellidos, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Edad", objEntidad.Edad, SqlDbType.Int);
                _conexion.AgregarParametro("@CorreoElectronico", objEntidad.CorreoElectronico, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Contrasena", objEntidad.Contrasena, SqlDbType.VarChar);
                _conexion.AgregarParametro("@IdRoles", objEntidad.Roles.IdRol, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Fecha", objEntidad.Fecha, SqlDbType.Date);
                _conexion.AbrirConexion();
                object resultado = _conexion.EjecutarEscalarSql(strComandoSql);
                IdUsuario = Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el Usuario en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return IdUsuario;
        }

        public int Modificar(Usuario objEntidad)
        {
            string strComandoSql = "UPDATE Usuarios SET Nombres = @Nombres, Apellidos = @Apellidos, Edad = @Edad, CorreoElectronico = @CorreoElectronico, Contrasena = @Contrasena, IdRoles = @IdRoles, Fecha = @Fecha WHERE IdUsuario = @IdUsuario";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdUsuario", objEntidad.IdUsuario, SqlDbType.Int);
                _conexion.AgregarParametro("@Nombres", objEntidad.Nombres, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Apellidos", objEntidad.Apellidos, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Edad", objEntidad.Edad, SqlDbType.Int);
                _conexion.AgregarParametro("@CorreoElectronico", objEntidad.CorreoElectronico, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Contrasena", objEntidad.Contrasena, SqlDbType.VarChar);
                _conexion.AgregarParametro("@IdRoles", objEntidad.Roles.IdRol, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Fecha", objEntidad.Fecha, SqlDbType.Date);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el Usuario en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        public int Eliminar(int IdUsuario)
        {
            string strComandoSql = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdUsuario", IdUsuario, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el Usuario de la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        public Usuario Consultar(UsuarioFiltro usuarioFiltro)
        {
            Usuario usuario = null;
            string consultaSql = "SELECT U.IdUsuario, U.Nombres, U.Apellidos, U.Edad, U.CorreoElectronico, U.Contrasena, R.IdRol, R.NombreRol AS Rol, U.Fecha " +
                "FROM Usuarios U INNER JOIN Roles R ON U.IdRoles = R.IdRol WHERE U.IdUsuario = @IdUsuario";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdUsuario", usuarioFiltro.IdUsuario, SqlDbType.Int);

                //_conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    usuario = UsuarioMapeo.Mapear(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar el Usuario en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return usuario;
        }


        public List<Usuario> ConsultarListado(UsuarioFiltro filtro)
        {
            List<Usuario> usuarios = new List<Usuario>();
            string consultaSql = "SELECT U.IdUsuario, U.Nombres, U.Apellidos, U.Edad, U.CorreoElectronico, U.Contrasena, R.IdRol, R.NombreRol AS Rol, U.Fecha " +
                "FROM Usuarios U INNER JOIN Roles R ON U.IdRoles = R.IdRol " +
                "WHERE u.Apellidos LIKE @Apellidos";

            if (!string.IsNullOrEmpty(filtro.CorreoElectronico))
            {
                consultaSql += " AND u.CorreoElectronico LIKE @CorreoElectronico";
            }

            if (filtro.IdUsuario > 0)
            {
                consultaSql += " AND u.IdUsuario = @IdUsuario";
            }

            if (filtro.IdRol > 0)
            {
                consultaSql += " AND R.IdRol = @IdRol";
            }

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@Apellidos", $"%{filtro.Apellidos}%", SqlDbType.VarChar);
                _conexion.AgregarParametro("@CorreoElectronico", $"%{filtro.CorreoElectronico}%", SqlDbType.VarChar);
                _conexion.AgregarParametro("@IdUsuario", filtro.IdUsuario, SqlDbType.Int);
                _conexion.AgregarParametro("@IdRol", filtro.IdRol, SqlDbType.Int);

                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    usuarios = UsuarioMapeo.MapearLista(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la lista de usuarios en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return usuarios;
        }

    }
}
