using AutoMapper;
using Mectronics.AdministracionEstudiantes.Transversales.Dtos;
using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using Mectronics.AdministracionEstudiantes.Transversales.Filtros;
using Mectronics.AdministracionEstudiantes.Transversales.Interfaces.IUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Servicio.Servicios
{
    public class UsuarioServicio : IUsuarioServicio
    {
        /// <summary>
        /// Instancia del repositorio de tiendas para acceder a la base de datos.
        /// </summary>
        private readonly IUsuarioRepositorio _repositorioUsuario;

        /// <summary>
        /// Instancia de AutoMapper para realizar conversiones entre entidades y DTOs.
        /// </summary>
        private readonly IMapper _mapeo;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="TiendaServicio"/> con un repositorio inyectado.
        /// </summary>
        /// <param name="repositorioTienda">Instancia del repositorio de tiendas.</param>
        /// <param name="mapeo">Instancia de AutoMapper para mapeo de entidades a DTOs.</param>
        public UsuarioServicio(IUsuarioRepositorio repositorioTienda, IMapper mapeo)
        {
            _repositorioUsuario = repositorioTienda;
            _mapeo = mapeo;
        }

        public UsuarioDto Autenticar(string correo, string contrasena)
        {
            var usuario = _repositorioUsuario.Autenticar(correo);

            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new UnauthorizedAccessException("La contraseña es requerida");
            }

            if (usuario == null)
                throw new UnauthorizedAccessException("El usuario no existe");

            if (contrasena != usuario.Contrasena)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta");
            }

            usuario.Contrasena = string.Empty;

            return _mapeo.Map<UsuarioDto>(usuario);
        }
        public UsuarioDto Consultar(UsuarioFiltro objFiltro)
        {
            if (objFiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            Usuario usuario = _repositorioUsuario.Consultar(objFiltro);
            UsuarioDto usuarioDto = _mapeo.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        public List<UsuarioDto> ConsultarListado(UsuarioFiltro objfiltro)
        {
            if (objfiltro == null)
                throw new ArgumentNullException("El filtro no puede ser nulo.");

            List<Usuario> usuarios = _repositorioUsuario.ConsultarListado(objfiltro);
            List<UsuarioDto> UsuarioDto = _mapeo.Map<List<UsuarioDto>>(usuarios);

            return UsuarioDto;
        }

        public int Eliminar(int IdUsuario)
        {
            if (IdUsuario <= 0)
                throw new ArgumentException("El ID de la tienda es inválido.");

            return _repositorioUsuario.Eliminar(IdUsuario);
        }

        public UsuarioDto Insertar(UsuarioDto usuarioDto)
        {
            Usuario usuario = _mapeo.Map<Usuario>(usuarioDto);

            //List<Usuario> usuarios = _repositorioUsuario.ConsultarListado(new UsuarioFiltro() { IdUsuario = usuarioDto.IdUsuarioEstudiante });

            //if (usuarios.Count > 3 )

            //throw new ArgumentException("El estudiante no puede tener inscribir mas de tres materias.");

            usuarioDto.IdUsuario = _repositorioUsuario.Insertar(usuario);

            return usuarioDto;
        }

        public UsuarioDto Modificar(UsuarioDto usuarioDto)
        {
            Usuario usuario = _mapeo.Map<Usuario>(usuarioDto);

            if (usuario.IdUsuario <= 0)
                throw new ArgumentException("El ID del Usuario es inválido.");

            //ValidarDatos(usuario);

            int actualizo = _repositorioUsuario.Modificar(usuario);

            if (actualizo <= 0)
                throw new ArgumentException("El registro no se actualizo.");

            return usuarioDto;
        }

    }
}
