using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IInscripcionMaterias;
using System;
using System.Collections.Generic;

namespace Mectronics.AdministracionEstudiantes.Servicio.Servicios
{
    /// <summary>
    /// Servicio para gestionar la inscripción de materias.
    /// </summary>
    public class InscripcionMateriaServicio : IInscripcionMateriaServicio
    {
        private readonly IInscripcionMateriaRepositorio _repositorioInscripcionMateria;
        private readonly IMapper _mapeo;

        /// <summary>
        /// Constructor de la clase InscripcionMateriaServicio.
        /// </summary>
        /// <param name="repositorioInscripcionMateria">Repositorio de inscripciones.</param>
        /// <param name="mapeo">Instancia de AutoMapper.</param>
        public InscripcionMateriaServicio(IInscripcionMateriaRepositorio repositorioInscripcionMateria, IMapper mapeo)
        {
            _repositorioInscripcionMateria = repositorioInscripcionMateria;
            _mapeo = mapeo;
        }

        /// <summary>
        /// Consulta una inscripción de materia según el filtro proporcionado.
        /// </summary>
        /// <param name="objFiltro">Filtro de búsqueda.</param>
        /// <returns>Objeto InscripcionMateriaDto con la información consultada.</returns>
        public InscripcionMateriaDto Consultar(InscripcionMateriaFiltro objFiltro)
        {
            if (objFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            InscripcionMateria inscripcionMateria = _repositorioInscripcionMateria.Consultar(objFiltro);
            InscripcionMateriaDto inscripcionMateriaDto = _mapeo.Map<InscripcionMateriaDto>(inscripcionMateria);

            return inscripcionMateriaDto;
        }

        /// <summary>
        /// Consulta una lista de inscripciones de materia según el filtro proporcionado.
        /// </summary>
        /// <param name="objEntidadDto">Filtro de búsqueda.</param>
        /// <returns>Lista de InscripcionMateriaDto.</returns>
        public List<InscripcionMateriaDto> ConsultarListado(InscripcionMateriaFiltro objEntidadDto)
        {
            if (objEntidadDto == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            List<InscripcionMateria> listaInscripciones = _repositorioInscripcionMateria.ConsultarListado(objEntidadDto);
            return _mapeo.Map<List<InscripcionMateriaDto>>(listaInscripciones);
        }

        /// <summary>
        /// Elimina una inscripción de materia por su ID.
        /// </summary>
        /// <param name="idInscripcionMateria">ID de la inscripción a eliminar.</param>
        /// <returns>Número de registros afectados.</returns>
        public int Eliminar(int idInscripcionMateria)
        {
            if (idInscripcionMateria <= 0)
                throw new ArgumentException("El ID de la inscripción es inválido.");

            return _repositorioInscripcionMateria.Eliminar(idInscripcionMateria);
        }

        /// <summary>
        /// Inserta una nueva inscripción de materia.
        /// </summary>
        /// <param name="objEntidadDto">Objeto con los datos de la inscripción.</param>
        /// <returns>Objeto InscripcionMateriaDto con el ID generado.</returns>
        public InscripcionMateriaDto Insertar(InscripcionMateriaDto objEntidadDto)
        {
            InscripcionMateria inscripcionMateria = _mapeo.Map<InscripcionMateria>(objEntidadDto);

            ValidarDatos(inscripcionMateria);

            objEntidadDto.IdInscripcion = _repositorioInscripcionMateria.Insertar(inscripcionMateria);

            return objEntidadDto;
        }

        /// <summary>
        /// Modifica una inscripción de materia existente.
        /// </summary>
        /// <param name="objEntidadDto">Objeto con los datos actualizados de la inscripción.</param>
        /// <returns>Objeto InscripcionMateriaDto actualizado.</returns>
        public InscripcionMateriaDto Modificar(InscripcionMateriaDto objEntidadDto)
        {
            InscripcionMateria inscripcionMateria = _mapeo.Map<InscripcionMateria>(objEntidadDto);

            if (inscripcionMateria.IdInscripcion <= 0)
                throw new ArgumentException("El ID de la inscripción es inválido.");

            ValidarDatos(inscripcionMateria);

            int resultado = _repositorioInscripcionMateria.Modificar(inscripcionMateria);
            if (resultado <= 0)
                throw new InvalidOperationException("No se pudo actualizar la inscripción.");

            return objEntidadDto;
        }

        /// <summary>
        /// Valida los datos de la inscripción de materia.
        /// </summary>
        /// <param name="objInscripcionMateria">Objeto a validar.</param>
        private void ValidarDatos(InscripcionMateria objInscripcionMateria)
        {
            if (objInscripcionMateria == null)
                throw new ArgumentNullException("La inscripción no puede ser nula.");

            if (objInscripcionMateria.Usuario == null || objInscripcionMateria.Usuario.IdUsuario <= 0)
                throw new ArgumentException("El usuario debe tener un ID válido.");

            if (objInscripcionMateria.Materia == null || objInscripcionMateria.Materia.IdMateria <= 0)
                throw new ArgumentException("La materia debe tener un ID válido.");
        }

    }
}
