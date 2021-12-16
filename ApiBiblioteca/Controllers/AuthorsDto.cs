using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Models.Dto
{
    public class AuthorsDto
    {
        public int id { get; set; }

        public int idBook { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public BooksDto book { get; set; }
    }

    
}
