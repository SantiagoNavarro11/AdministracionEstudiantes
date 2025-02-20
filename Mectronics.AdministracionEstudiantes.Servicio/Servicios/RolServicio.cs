using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IRol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Servicio.Servicios
{
    public class RolServicio : IRolServicio
    {
        /// <summary>
        /// Instancia del repositorio de tiendas para acceder a la base de datos.
        /// </summary>
        private readonly IRolRepositorio _repositorioTienda;

        /// <summary>
        /// Instancia de AutoMapper para realizar conversiones entre entidades y DTOs.
        /// </summary>
        private readonly IMapper _mapeo;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="TiendaServicio"/> con un repositorio inyectado.
        /// </summary>
        /// <param name="repositorioTienda">Instancia del repositorio de tiendas.</param>
        /// <param name="mapeo">Instancia de AutoMapper para mapeo de entidades a DTOs.</param>
        public RolServicio(IRolRepositorio repositorioTienda, IMapper mapeo)
        {
            _repositorioTienda = repositorioTienda;
            _mapeo = mapeo;
        }

        public RolDto Consultar(RolFiltro rolFiltro)
        {
            if (rolFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            Rol rol = _repositorioTienda.Consultar(rolFiltro);
            RolDto rolDto = _mapeo.Map<RolDto>(rol);

            return rolDto;
        }

        public List<RolDto> ConsultarLista(RolFiltro rolFiltro)
        {
            if (rolFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            List<Rol> rol = _repositorioTienda.ConsultarListado(rolFiltro);
            List<RolDto> rolDto = _mapeo.Map<List<RolDto>>(rol);

            return rolDto;
        }

        //private void ValidarDatos(Rol rol)
        //{
        //    if (rol == null)
        //        throw new ArgumentNullException("La registro no puede ser nulo.");

        //    if (rol.IdRol == null || rol.IdRol <= 0)
        //        throw new ArgumentException("El Id de la tienda no puede estar vacío.");

        //    if (string.IsNullOrWhiteSpace(rol.Nombre))
        //        throw new ArgumentException("El nombre de la tienda no puede estar vacío.");
        //}
    }
}
