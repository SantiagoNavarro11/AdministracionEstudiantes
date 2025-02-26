﻿using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Dtos
{
    public class UsuarioDto
    { /// <summary>
      /// Identificador único del usuario.
      /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Edad del usuario.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Identificador del rol asignado al usuario.
        /// </summary>
        public Rol Roles { get; set; }

        /// <summary>
        /// Fecha de registro del usuario.
        /// </summary>
        public DateTime Fecha { get; set; }


        public UsuarioDto()
        {
            IdUsuario = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            Edad = 0;
            CorreoElectronico = string.Empty;
            Contrasena = string.Empty;
            Roles = new Rol();
            Fecha = DateTime.MinValue;
        }
    }

}
