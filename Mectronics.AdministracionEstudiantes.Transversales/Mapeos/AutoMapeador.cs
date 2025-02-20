using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Mapeos
{
    public class AutoMapeador : Profile
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="AutoMapeador"/> y configura los mapeos.
        /// </summary>
        public AutoMapeador()
        {
            /// <summary>
            /// Configura el mapeo bidireccional entre entidades y DTOs.
            /// </summary>
            CreateMap<Rol, RolDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Materia, MateriaDto>().ReverseMap();
        }
    }
}
