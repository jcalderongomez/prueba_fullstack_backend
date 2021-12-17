using AutoMapper;
using ApiBiblioteca.Data;
using ApiBiblioteca.Models;
using ApiBiblioteca.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.BulkExtensions;

namespace ApiBiblioteca.Repositorio
{
    public class BooksRepositorio : IBooksRepositorio
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public BooksRepositorio(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BooksDto> CreateUpdate(BooksDto booksDto)
        {
            Books book = _mapper.Map<BooksDto, Books>(booksDto);
            if (book.Id > 0)
            {
                _db.Books.Update(book);
            }
            else
            {
                await _db.Books.AddAsync(book);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Books, BooksDto>(book);

        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                Books book = await _db.Books.FindAsync(id);
                if (book == null)
                {
                    return false;
                }
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                return true;
            }   
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<BooksDto> GetBookById(int Id)
        {
            Books book = await _db.Books.FindAsync(Id);
            return _mapper.Map<BooksDto>(book);
        }

        public async Task<BooksDto> GetBooksAuthById(FindBook findbook)
        {
            /*
            var books = await (from auth in  _db.Authors
                                           join book in _db.Books on auth.idBook equals book.Id
                                           where auth.id == findbook.idAuth || (book.publishDate>=findbook.finicio && book.publishDate<=findbook.ffin
                                           )
                               select book).FirstOrDefaultAsync();
*/

            var books = await (from book in  _db.Books
                                           where book.publishDate>=findbook.finicio && book.publishDate<=findbook.ffin select book).FirstOrDefaultAsync();

            return _mapper.Map<BooksDto>(books);
        }
        public async Task<bool> CreateBooks(List<Books> books)
        {
            try
            {
                await _db.BulkInsertAsync(books);
            }
            catch(Exception e)
            {
                return false;
            }             
            return true;
        }

        public async Task<List<BooksDto>> GetBooks()
        {
            List<Books> lista = await _db.Books.ToListAsync();
            return _mapper.Map<List<BooksDto>>(lista);
        }
    }

}
