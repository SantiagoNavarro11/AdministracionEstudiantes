using Mectronics.AdministracionEstudiantes.Transversales.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mectronics.AdministracionEstudiantes.Transversales.Dtos
{
    public class EventoLogDto
    {

        public int IdLogs { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Informacion { get; set; }
        public Usuario Usuario { get; set; }
    }
}
