using ApiBiblioteca.Models;
using ApiBiblioteca.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiBiblioteca.Repositorio
{
    public interface IBooksRepositorio
    {
        Task<List<BooksDto>> GetBooks();

        Task<BooksDto> GetBookById(int Id);

        Task<BooksDto> GetBooksAuthById(FindBook Id);
        Task<bool> CreateBooks(List<Books> books);
        Task<BooksDto> CreateUpdate(BooksDto bookDto);

        Task<bool> DeleteBook(int id);
    }
}
