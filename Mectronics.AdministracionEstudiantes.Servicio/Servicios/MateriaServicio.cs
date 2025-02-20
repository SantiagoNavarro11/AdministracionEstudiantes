using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IMateria;

namespace Mectronics.AdministracionEstudiantes.Servicio
{
    public class MateriaServicio : IMateriaServicio
    {
        private readonly IMateriaRepositorio _repositorioMateria;
        private readonly IMapper _mapeo;

        public MateriaServicio(IMateriaRepositorio repositorioMateria, IMapper mapeo)
        {
            _repositorioMateria = repositorioMateria;
            _mapeo = mapeo;
        }

        public MateriaDto Insertar(MateriaDto materiaDto)
        {
            Materia materia = _mapeo.Map<Materia>(materiaDto);
            ValidarDatos(materia);
            materiaDto.IdMateria = _repositorioMateria.Insertar(materia);
            return materiaDto;
        }

        public MateriaDto Actualizar(MateriaDto materiaDto)
        {
            Materia materia = _mapeo.Map<Materia>(materiaDto);

            if (materia.IdMateria <= 0)
                throw new ArgumentException("El ID de la materia es inválido.");

            ValidarDatos(materia);

            int actualizo = _repositorioMateria.Modificar(materia);

            if (actualizo <= 0)
                throw new ArgumentException("El registro no se actualizó.");

            return materiaDto;
        }

        public int Eliminar(int idMateria)
        {
            if (idMateria <= 0)
                throw new ArgumentException("El ID de la materia es inválido.");

            return _repositorioMateria.Eliminar(idMateria);
        }

        public MateriaDto Consultar(MateriaFiltro objFiltro)
        {
            if (objFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            Materia materia = _repositorioMateria.Consultar(objFiltro);
            MateriaDto materiaDto = _mapeo.Map<MateriaDto>(materia);
            return materiaDto;
        }

        public List<MateriaDto> ConsultarLista(MateriaFiltro objFiltro)
        {
            if (objFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            List<Materia> materias = _repositorioMateria.ConsultarListado(objFiltro);
            List<MateriaDto> materiasDto = _mapeo.Map<List<MateriaDto>>(materias);
            return materiasDto;
        }

        private void ValidarDatos(Materia materia)
        {
            if (materia == null)
                throw new ArgumentNullException("El registro de la materia no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(materia.Nombre))
                throw new ArgumentException("El nombre de la materia no puede estar vacío.");

            if (materia.Nombre.Length < 3)
                throw new ArgumentException("El nombre de la materia debe tener al menos 3 caracteres.");

            if (materia.IdUsuarioProfesor == null || materia.IdUsuarioProfesor <= 0)
                throw new ArgumentException("El ID del profesor debe ser un valor válido mayor que 0.");

            if (materia.NumeroCreditos <= 0)
                throw new ArgumentException("El número de créditos debe ser mayor a 0.");
        }

    }
}