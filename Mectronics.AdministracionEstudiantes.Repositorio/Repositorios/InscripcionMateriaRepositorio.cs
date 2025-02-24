using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IInscripcionMaterias;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRepositorio;
using Mectronics.AdministracionEstudiantes.Transversales.Mapeos;
using System.Data;

namespace Mectronics.AdministracionEstudiantes.Repositorio.Repositorios
{
    public class InscripcionMateriaRepositorio : IInscripcionMateriaRepositorio
    {
        private readonly IConexionBaseDatos _conexion;

        public InscripcionMateriaRepositorio(IConexionBaseDatos conexion)
        {
            _conexion = conexion;
        }

        public int Insertar(InscripcionMateria objEntidad)
        {
            string strComandoSql = "INSERT INTO InscripcionMaterias (IdUsuario, IdMateria) VALUES (@IdUsuario, @IdMateria)";
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

        public int Modificar(InscripcionMateria objEntidad)
        {
            string strComandoSql = "UPDATE InscripcionMaterias SET IdMateria = @IdMateria WHERE IdUsuario = @IdUsuario";
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

        public int Eliminar(int IdInscripcion)
        {
            string strComandoSql = "DELETE FROM InscripcionMaterias WHERE IdInscripcion = @IdInscripcion";
            int filasAfectadas = 0;

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdInscripcion", IdInscripcion, SqlDbType.Int);
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

        public InscripcionMateria Consultar(InscripcionMateriaFiltro filtro)
        {
            InscripcionMateria inscripcionMateria = null;

            string consultaSql = @"SELECT i.IdInscripcion,u.Nombres AS Usuario, m.NombreMateria AS Materia FROM InscripcionMaterias i INNER JOIN Usuarios u ON i.IdUsuario = u.IdUsuario INNER JOIN Materias m ON i.IdMateria = m.IdMateria WHERE i.IdInscripcion = @IdInscripcion";

            try
            {
                _conexion.LimpiarParametros();
                _conexion.AgregarParametro("@IdInscripcion", filtro.IdInscripcion, SqlDbType.Int);
                _conexion.AbrirConexion();

                using (IDataReader resultado = _conexion.EjecutarConsulta(consultaSql))
                {
                    inscripcionMateria = InscripcionMateriaMapeo.Mapear(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la inscripción en la base de datos.", ex);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
            return inscripcionMateria;
        }



        public List<InscripcionMateria> ConsultarListado(InscripcionMateriaFiltro filtro)
        {
            List<InscripcionMateria> inscripcionesMaterias = new List<InscripcionMateria>();
            string consultaSql = "SELECT i.IdInscripcion, i.IdUsuario, u.Nombres AS NombreUsuario, u.Apellidos AS ApellidoUsuario, i.IdMateria, m.NombreMateria AS Materia, m.IdUsuarioProfesor, " +
                "p.Nombres + ' ' +  p.Apellidos As Profesor, m.NumeroCreditosMateria FROM InscripcionMaterias i INNER JOIN Usuarios u ON i.IdUsuario = u.IdUsuario INNER JOIN Materias m ON i.IdMateria = m.IdMateria " +
                "INNER JOIN Usuarios p ON m.IdUsuarioProfesor = p.IdUsuario";

            if (filtro.IdUsuario > 0)
            {
                consultaSql += " AND i.IdUsuario = @IdUsuario";
            }

            if (filtro.IdProfesor > 0)
            {
                consultaSql += " AND m.IdUsuarioProfesor = @IdProfesor";
            }

            if (filtro.IdMateria > 0)
            {
                consultaSql += " AND i.IdMateria = @IdMateria";
            }

            try
            {
                _conexion.LimpiarParametros();

                _conexion.AgregarParametro("@IdUsuario", filtro.IdUsuario, SqlDbType.Int);
                _conexion.AgregarParametro("@IdProfesor", filtro.IdProfesor, SqlDbType.Int);
                _conexion.AgregarParametro("@IdMateria", filtro.IdMateria, SqlDbType.Int);

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
