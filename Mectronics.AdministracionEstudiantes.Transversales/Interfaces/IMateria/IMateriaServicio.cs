using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using System.Collections.Generic;

namespace Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria
{
    public interface IMateriaServicio
    {
        /// <summary>
        /// Inserta una nueva materia en la base de datos.
        /// </summary>
        /// <param name="materiaDto">Objeto <see cref="MateriaDto"/> con la información a insertar.</param>
        /// <returns>Información del registro creado.</returns>
        MateriaDto Insertar(MateriaDto materiaDto);

        /// <summary>
        /// Actualiza una materia existente en la base de datos.
        /// </summary>
        /// <param name="materiaDto">Objeto <see cref="MateriaDto"/> con la información actualizada.</param>
        /// <returns>Información del registro modificado.</returns>
        MateriaDto Actualizar(MateriaDto materiaDto);

        /// <summary>
        /// Elimina una materia por su identificador.
        /// </summary>
        /// <param name="idMateria">Identificador único de la materia a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>
        int Eliminar(int idMateria);

        /// <summary>
        /// Consulta una materia basada en los filtros proporcionados.
        /// </summary>
        /// <param name="objFiltro">Objeto <see cref="MateriaFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Registro encontrado.</returns>
        MateriaDto Consultar(MateriaFiltro objFiltro);

        /// <summary>
        /// Consulta una lista de materias según los filtros proporcionados.
        /// </summary>
        /// <param name="objFiltro">Objeto <see cref="MateriaFiltro"/> con los criterios de búsqueda.</param>
        /// <returns>Lista de registros encontrados.</returns>
        List<MateriaDto> ConsultarLista(MateriaFiltro objFiltro);
    }
}
