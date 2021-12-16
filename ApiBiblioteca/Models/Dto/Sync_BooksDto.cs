using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Models.Dto
{
    public class Sync_BooksDto
    {
        public int operationId { get; set; }

        public List<BooksDto> Books { get; set; }
    }
}
