using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PDFMerge.Models
{
    
    public class Materia
    {
        public int MateriaId { get; set; }

        [DisplayName("Materia")]
        public string Descripcion { get; set; }
    }
}