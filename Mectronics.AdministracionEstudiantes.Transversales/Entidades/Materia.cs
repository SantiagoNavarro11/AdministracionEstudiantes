﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Entidades
{
    public class Materia
    {
        /// <summary>
        /// Identificador único de la materia.
        /// </summary>
        public int IdMateria { get; set; }

        /// <summary>
        /// Nombre de la materia.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Número de créditos de la materia.
        /// </summary>
        public int NumeroCreditos { get; set; }

        /// <summary>
        /// Identificador del profesor encargado de la materia.
        /// </summary>
        public int IdUsuarioProfesor { get; set; }

        /// <summary>
        /// Nombre del profesor encargado de la materia.
        /// </summary>
        public string NombreProfesor { get; set; }

        /// <summary>
        /// Constructor de la clase <see cref="Materia"/>.
        /// Inicializa una nueva instancia con valores predeterminados.
        /// </summary>
        public Materia()
        {
            IdMateria = 0;
            Nombre = string.Empty;
        }
    }
}
