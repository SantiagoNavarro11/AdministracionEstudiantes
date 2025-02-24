using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Repositorio
{
    public class ConexionBaseDatos : IConexionBaseDatos
    {
        /// <summary>
        /// Representa el comando SQL asociado a la conexión.
        /// </summary>
        public SqlCommand Comando { get; set; }

        /// <summary>
        /// Representa la conexión activa con la base de datos.
        /// </summary>
        public SqlConnection Conexion { get; set; }

        /// <summary>
        /// Representa la transacción activa en la base de datos.
        /// </summary>
        public SqlTransaction Transaccion { get; set; }


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ConexionBaseDatos"/>.
        /// Obtiene la cadena de conexión desde la configuración.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación que contiene la cadena de conexión.</param>
        /// <exception cref="Exception">Se lanza si no existe un archivo de configuración.</exception>
        public ConexionBaseDatos(IConfiguration configuration)
        {
            if (configuration == null)
                throw new Exception("No existe un archivo de configuración.");

            string cadenaConexion = configuration.GetConnectionString("AdministracionEstudiantes");

            Conexion = new SqlConnection(cadenaConexion);
            Comando = Conexion.CreateCommand();
            Comando.Connection = Conexion;
        }

        /// <summary>
        /// Abre la conexión con la base de datos si no está abierta.
        /// </summary>
        public void AbrirConexion()
        {
            if (Conexion.State != ConnectionState.Open)
            {
                Conexion.Open();
            }
        }

        /// <summary>
        /// Cierra la conexión con la base de datos y libera los recursos instanciados.
        /// </summary>
        public void CerrarConexion()
        {
            if (Conexion != null && Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }
        }
        /// <summary>
        /// Ejecuta una consulta SQL y devuelve un conjunto de resultados.
        /// </summary>
        /// <param name="consultaSql">Consulta SQL a ejecutar.</param>
        /// <param name="usarTransaccion">Indica si la consulta debe ejecutarse dentro de una transacción. Por defecto es <c>false</c>.</param>
        /// <returns>Un <see cref="IDataReader"/> con los resultados de la consulta.</returns>
        public IDataReader EjecutarConsulta(string consultaSql, bool usarTransaccion = false)
        {
            AbrirConexion();

            if (Comando != null)
            {
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = consultaSql;

                if (usarTransaccion)
                    Comando.Transaction = Transaccion;

                return Comando.ExecuteReader();
            }

            return null;
        }

        /// <summary>
        /// Ejecuta un comando SQL de tipo <c>INSERT</c>, <c>UPDATE</c> o <c>DELETE</c>.
        /// </summary>
        /// <param name="comandoSql">Comando SQL a ejecutar.</param>
        /// <param name="usarTransaccion">Indica si el comando debe ejecutarse dentro de una transacción. Por defecto es <c>false</c>.</param>
        /// <returns>El número de filas afectadas por la ejecución del comando.</returns>
        public int EjecutarComando(string comandoSql, bool usarTransaccion = false)
        {
            AbrirConexion();

            if (Comando != null)
            {
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = comandoSql;

                if (usarTransaccion)
                    Comando.Transaction = Transaccion;

                return Comando.ExecuteNonQuery();
            }

            return 0;
        }

        /// <summary>
        /// Agrega un parámetro al comando SQL actual.
        /// </summary>
        /// <param name="nombre">Nombre del parámetro en la consulta SQL.</param>
        /// <param name="valor">Valor del parámetro.</param>
        /// <param name="sqlTipo">Tipo de dato del parámetro definido en <see cref="SqlDbType"/>.</param>
        public void AgregarParametro(string nombre, object valor, SqlDbType sqlTipo)
        {
            if (Comando != null)
            {
                Comando.Parameters.Add(nombre, sqlTipo).Value = valor;
                Comando.Parameters[nombre].IsNullable = true;
            }
        }

        /// <summary>
        /// Elimina todos los parámetros asociados al comando SQL actual.
        /// </summary>
        public void LimpiarParametros()
        {
            Comando?.Parameters.Clear();
        }

        /// <summary>
        /// Libera los recursos instanciados de la base de datos.
        /// </summary>
        public void Dispose()
        {
            CerrarConexion();
        }

        public object EjecutarEscalarSql(string comandoSql)
        {
            AbrirConexion();

            if (Comando != null)
            {
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = comandoSql;
                return Comando.ExecuteScalar();
            }

            return 0;
        }

    }
}