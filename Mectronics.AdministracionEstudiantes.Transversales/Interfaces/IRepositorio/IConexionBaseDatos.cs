using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio
{
    /// <summary>
    /// Define la estructura para gestionar la conexión con la base de datos
    /// y la ejecución de comandos SQL de manera abstracta.
    /// </summary>
    public interface IConexionBaseDatos : IDisposable
    {/// <summary>
     /// Define la estructura para gestionar la conexión con la base de datos
     /// y la ejecución de comandos SQL de manera abstracta.
     /// </summary>
        public interface IConexionBaseDatos : IDisposable
        {
            /// <summary>
            /// Obtiene o establece el comando SQL asociado a la conexión.
            /// </summary>
            SqlCommand Comando { get; set; }

            /// <summary>
            /// Obtiene o establece la conexión SQL activa.
            /// </summary>
            SqlConnection Conexion { get; set; }

            /// <summary>
            /// Abre la conexión con la base de datos si no está abierta.
            /// </summary>
            void AbrirConexion();

            /// <summary>
            /// Cierra la conexión con la base de datos y libera los recursos utilizados.
            /// </summary>
            void CerrarConexion();

            /// <summary>
            /// Ejecuta una consulta SQL que devuelve un conjunto de resultados en forma de <see cref="IDataReader"/>.
            /// </summary>
            /// <param name="consultaSql">Consulta SQL a ejecutar.</param>
            /// <param name="usarTransaccion">Indica si la consulta debe ejecutarse dentro de una transacción. El valor predeterminado es <c>false</c>.</param>
            /// <returns>Un <see cref="IDataReader"/> con los resultados de la consulta.</returns>
            IDataReader EjecutarConsulta(string consultaSql, bool usarTransaccion = false);

            /// <summary>
            /// Ejecuta un comando SQL de tipo <c>INSERT</c>, <c>UPDATE</c> o <c>DELETE</c>.
            /// </summary>
            /// <param name="comandoSql">Comando SQL a ejecutar.</param>
            /// <param name="usarTransaccion">Indica si el comando debe ejecutarse dentro de una transacción. El valor predeterminado es <c>false</c>.</param>
            /// <returns>El número de filas afectadas por la ejecución del comando.</returns>
            int EjecutarComando(string comandoSql, bool usarTransaccion = false);

            /// <summary>
            /// Agrega un parámetro al comando SQL actual.
            /// </summary>
            /// <param name="nombre">Nombre del parámetro en la consulta SQL.</param>
            /// <param name="valor">Valor del parámetro.</param>
            /// <param name="sqlTipo">Tipo de dato del parámetro definido en <see cref="SqlDbType"/>.</param>
            void AgregarParametro(string nombre, object valor, SqlDbType sqlTipo);

            /// <summary>
            /// Elimina todos los parámetros asociados al comando SQL actual.
            /// </summary>
            void LimpiarParametros();
        }
    }
}
