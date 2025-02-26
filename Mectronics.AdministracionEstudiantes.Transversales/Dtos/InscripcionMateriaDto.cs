﻿using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Dtos
{
    public class InscripcionMateriaDto
    {
        /// <summary>
        /// Identificador único de la inscripción.
        /// </summary>
        public int IdInscripcion { get; set; }

        /// <summary>
        /// Identificador del usuario inscrito en la materia.
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Identificador de la materia en la que se inscribió el usuario.
        /// </summary>
        public Materia Materia { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="InscripcionMateria"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public InscripcionMateriaDto()
        {
            IdInscripcion = 0;
            Usuario = new Usuario();
            Materia = new Materia();
        }
    }
}
