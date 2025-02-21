using Mectronics.AdministracionEstudiantes.Transversales;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    /// <summary>
    /// Repositorio para la gestión de materias en la base de datos.
    /// </summary>
    public class MateriaRepositorio : IMateriaRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        /// <summary>
        /// Constructor de la clase MateriaRepositorio.
        /// </summary>
        /// <param name="conexion">Instancia de la conexión a la base de datos.</param>
        public MateriaRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }

        /// <summary>
        /// Inserta una nueva materia en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto que contiene la información de la materia.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int Insertar(Materia objEntidad)
        {
            string strComandoSql = @"INSERT INTO Materias (NombreMateria, NumeroCreditosMateria, IdUsuarioProfesor) 
                                     VALUES (@NombreMateria, @NumeroCreditosMateria, @IdUsuarioProfesor)";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@NombreMateria", objEntidad.Nombre, SqlDbType.VarChar);
                _conexion.AgregarParametro("@NumeroCreditosMateria", 3, SqlDbType.Int);
                _conexion.AgregarParametro("@IdUsuarioProfesor", objEntidad.IdUsuarioProfesor, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la materia en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Modifica una materia existente en la base de datos.
        /// </summary>
        /// <param name="objEntidad">Objeto que contiene la información actualizada de la materia.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int Modificar(Materia objEntidad)
        {
            string strComandoSql = @"UPDATE Materias 
                             SET NombreMateria = @Nombre, 
                                 IdUsuarioProfesor = @IdUsuarioProfesor 
                             WHERE IdMateria = @IdMateria";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdMateria", objEntidad.IdMateria, SqlDbType.Int);
                _conexion.AgregarParametro("@Nombre", objEntidad.Nombre, SqlDbType.VarChar);
                //_conexion.AgregarParametro("@NumeroCreditosMateria", objEntidad.NumeroCreditos, SqlDbType.Int);
                _conexion.AgregarParametro("@IdUsuarioProfesor", objEntidad.IdUsuarioProfesor, SqlDbType.Int);
                _conexion.AbrirConexion();

                filasAfectadas = _conexion.EjecutarComando(strComandoSql);

                if (filasAfectadas == 0)
                    throw new Exception("No se actualizó ninguna fila. Verifica que el IdMateria existe en la base de datos.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la materia en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }


        /// <summary>
        /// Elimina una materia de la base de datos.
        /// </summary>
        /// <param name="IdMateria">Identificador de la materia a eliminar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public int Eliminar(int IdMateria)
        {
            string strComandoSql = "DELETE FROM Materias WHERE IdMateria = @IdMateria";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdMateria", IdMateria, SqlDbType.Int);
                _conexion.AbrirConexion();
                filasAfectadas = _conexion.EjecutarComando(strComandoSql);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la materia de la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Consulta una materia específica en la base de datos.
        /// </summary>
        /// <param name="objMateria">Filtro con el identificador de la materia a consultar.</param>
        /// <returns>Objeto Materia con la información consultada.</returns>
        public Materia Consultar(MateriaFiltro objMateria)
        {
            Materia materia = null;
            string consultaSql = "SELECT IdMateria, NombreMateria, NumeroCreditosMateria, IdUsuarioProfesor FROM Materias WHERE IdMateria = @IdMateria";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdMateria", objMateria.IdMateria, SqlDbType.Int);
                _conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    materia = MateriaMapeo.Mapear(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la materia en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return materia;
        }

        /// <summary>
        /// Consulta el listado de materias en la base de datos.
        /// </summary>
        /// <returns>Lista de objetos Materia registrados.</returns>
        public List<Materia> ConsultarListado(MateriaFiltro objMateria)
        {
            List<Materia> materias = new List<Materia>();
            string consultaSql = @" SELECT m.IdMateria, m.NombreMateria, m.NumeroCreditosMateria, m.IdUsuarioProfesor FROM Materias m INNER JOIN Usuarios u ON m.IdUsuarioProfesor = u.IdUsuario";

            if (objMateria.IdMateria > 0)
            {
                consultaSql += " AND m.IdMateria = @IdMateria";
            }

            if (!string.IsNullOrWhiteSpace(objMateria.Nombre))
            {
                consultaSql += " AND m.NombreMateria LIKE @NombreMateria";
            }

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdMateria", objMateria.IdMateria, SqlDbType.Int);
                _conexion.AgregarParametro("@NombreMateria", "%" + objMateria.Nombre + "%", SqlDbType.VarChar);
                _conexion.AbrirConexion();
                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    materias = MateriaMapeo.MapearLista(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la lista de materias en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return materias;
        }


    }
}
