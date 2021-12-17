using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Models.Dto
{
    public class FindBook
    {
        public int idAuth { get; set; }
        public DateTime finicio { get; set; }
        public DateTime ffin { get; set; }
    }
}
