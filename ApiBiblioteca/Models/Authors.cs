using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Models
{
    public class Authors
    {
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("idBook")]
        [RegularExpression("[0-9]*", ErrorMessage = "Solo se permiten números")]
        public int idBook { get; set; }


        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string lastName { get; set; }

        public virtual Books book { get; set; }

        public virtual List<Books> Books{ get; set; }

}

    
}
