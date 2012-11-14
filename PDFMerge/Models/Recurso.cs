using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDFMerge.Models
{
    public class Recurso
    {
        public int RecursoId { get; set; }
        
        [Required]
        public string Nombre { get; set; }

        public string Comentario { get; set; }
        
        [DisplayName("Archivo")]
        public string codigo { get; set; }

        public int CursoId { get; set; }

        public virtual Curso Curso { get; set; }

        public int MateriaId { get; set; }

        public virtual Materia Materia { get; set; }
    }
}