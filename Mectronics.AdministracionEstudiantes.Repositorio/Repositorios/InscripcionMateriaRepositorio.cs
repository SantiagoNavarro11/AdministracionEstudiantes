using Mectronics.AdministracionEstudiantes.Transversales;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorioInscripcionMaterias;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    /// <summary>
    /// Repositorio para la gestión de inscripciones de materias en la base de datos.
    /// </summary>
    public class InscripcionMateriaRepositorio : IInscripcionMateriaRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        /// <summary>
        /// Constructor que inicializa el repositorio con una conexión a la base de datos.
        /// </summary>
        /// <param name="conexion">Instancia de conexión proporcionada por inyección de dependencias.</param>
        public InscripcionMateriaRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Inserta una nueva inscripción de materia en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto que contiene la información de la inscripción.</param>
        /// <returns>Número de filas afectadas por la operación.</returns>
        public int Insertar(InscripcionMateria objEntidad)
        {
            string strComandoSql = @"INSERT INTO InscripcionMaterias (IdUsuario, IdMateria) VALUES (@IdUsuario, @IdMateria)";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdUsuario", objEntidad.Usuario.IdUsuario, SqlDbType.Int);
                _conexion.AgregarParametro("@IdMateria", objEntidad.Materia.IdMateria, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la inscripción en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Modifica una inscripción de materia existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto que contiene la información actualizada de la inscripción.</param>
        /// <returns>Número de filas afectadas por la operación.</returns>
        public int Modificar(InscripcionMateria objEntidad)
        {
            string strComandoSql = @"UPDATE InscripcionMaterias SET IdMateria = @IdMateria WHERE IdUsuario = @IdUsuario";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdUsuario", objEntidad.Usuario.IdUsuario, SqlDbType.Int);
                _conexion.AgregarParametro("@IdMateria", objEntidad.Materia.IdMateria, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la inscripción en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Elimina una inscripción de materia de la base de datos.
        /// </summary>
        /// <param name="IdUsuario">Identificador del usuario cuya inscripción será eliminada.</param>
        /// <returns>Número de filas afectadas por la operación.</returns>
        public int Eliminar(int IdUsuario)
        {
            string strComandoSql = @"DELETE FROM InscripcionMaterias WHERE IdUsuario = @IdUsuario AND IdMateria = @IdMateria";
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
                throw new Exception("Error al eliminar la inscripción de la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Consulta el listado de inscripciones de materias en la base de datos.
        /// </summary>
        /// <returns>Lista de inscripciones de materias registradas.</returns>
        public List<InscripcionMateria> ConsultarListado()
        {
            List<InscripcionMateria> inscripcionesMaterias = new List<InscripcionMateria>();
            string consultaSql = @"SELECT IdUsuario, IdMateria FROM InscripcionMaterias";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    inscripcionesMaterias = InscripcionMateriaMapeo.MapearLista(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la lista de inscripciones en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return inscripcionesMaterias;
        }
    }
}
