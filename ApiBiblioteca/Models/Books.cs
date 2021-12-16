using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        ///[RegularExpression("[A-Za-z ñÑ ÁáÉéÍíÓóÚú]*", ErrorMessage = "Solo se permiten letras")]
        public string title { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string description { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Solo se permiten números")]
        public int pageCount { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string excerpt { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime? publishDate { get; set; }

    }
}
