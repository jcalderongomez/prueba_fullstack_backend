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
    public class AuthorsRepostorio : IAuthorsRepositorio
    {
            private readonly ApplicationDbContext _db;
            private IMapper _mapper;

            public AuthorsRepostorio(ApplicationDbContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }
            public async Task<AuthorsDto> CreateUpdate(AuthorsDto authorsDto)
            {
                Authors author = _mapper.Map<AuthorsDto, Authors>(authorsDto);
                if (author.id > 0)
                {
                    _db.Authors.Update(author);
                }
                else
                {
                    await _db.Authors.AddAsync(author);
                }
                await _db.SaveChangesAsync();
                return _mapper.Map<Authors, AuthorsDto>(author);
            }

            public async Task<bool> DeleteAuthor(int id)
            {
                try
                {
                    Authors author = await _db.Authors.FindAsync(id);
                    if (author == null)
                    {
                        return false;
                    }
                    _db.Authors.Remove(author);
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        public async Task<bool> CreateAuthors(List<Authors> authors)
        {
            try
            {
                await _db.BulkInsertAsync(authors);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public async Task<AuthorsDto> GetAuthorById(int Id)
            {
                Authors author = await _db.Authors.FindAsync(Id);
                return _mapper.Map<AuthorsDto>(author);
            }

            public async Task<List<AuthorsDto>> GetAuthors()
            {
                List<Authors> lista = await _db.Authors.Include(x=>x.book).ToListAsync();
                return _mapper.Map<List<AuthorsDto>>(lista);
            }
        }
}
