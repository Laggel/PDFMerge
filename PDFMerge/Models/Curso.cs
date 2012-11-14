using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PDFMerge.Models
{
    public class Curso
    {
        public int CursoId { get; set; }

        [DisplayName("Curso")]
        public string Descripcion { get; set; }
    }
}