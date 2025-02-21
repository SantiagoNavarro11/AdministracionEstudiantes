using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IEventoLog;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    public class EventoLogRepositorio : IEventoLogRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        /// <summary>
        /// Constructor que inicializa una instancia de <see cref="RolRepositorio"/> con una conexión a la base de datos.
        /// </summary>
        /// <param name="conexion">Instancia de <see cref="IConexionBaseDatos"/> proporcionada por inyección de dependencias.</param>
        public EventoLogRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }
        public int Insertar(EventoLog objEntidad)

        {
            string strComandoSql = @"INSERT INTO EventosLogs (TipoLog, Fecha, Informacion, IdUsuario) 
                                 VALUES (@TipoLog, @Fecha, @Informacion, @IdUsuario)";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@TipoLog", objEntidad.Tipo, SqlDbType.VarChar);
                _conexion.AgregarParametro("@Fecha", objEntidad.Fecha, SqlDbType.Date);
                _conexion.AgregarParametro("@Informacion", objEntidad.Informacion, SqlDbType.VarChar);
                _conexion.AgregarParametro("@IdUsuario", objEntidad.Usuario.IdUsuario, SqlDbType.Int);

                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el evento en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return filasAfectadas;
        }
        public List<EventoLog> ConsultarListado(EventoLogFiltro objEntidad)
        {
            List<EventoLog> eventos = new List<EventoLog>();
            string consultaSql = @"SELECT e.IdLogs, e.TipoLog, e.Fecha, e.Informacion, e.IdUsuario 
                           FROM EventosLogs e INNER JOIN Usuarios u ON e.IdUsuario = u.IdUsuario WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(objEntidad.TipoLog))
            {
                consultaSql += " AND e.TipoLog LIKE @TipoLog";
            }

            if (!objEntidad.Fecha.HasValue)
            {
                consultaSql += " AND e.Fecha = @Fecha";
            }

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@TipoLog", "%" + objEntidad.TipoLog + "%", SqlDbType.VarChar);
                _conexion.AgregarParametro("@Fecha", objEntidad.Fecha ?? (object)DBNull.Value, SqlDbType.Date);

                _conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    eventos = EventoLogMapeo.MapearLista(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los eventos en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return eventos;
        }
    }
}


