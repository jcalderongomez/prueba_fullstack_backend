using ApiBiblioteca.Models;
using ApiBiblioteca.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Repositorio
{
	public interface IAuthorsRepositorio
	{
		Task<List<AuthorsDto>> GetAuthors();

		Task<AuthorsDto> GetAuthorById(int Id);
		Task<bool> CreateAuthors(List<Authors> authors);
		Task<AuthorsDto> CreateUpdate(AuthorsDto authorDto);

		Task<bool> DeleteAuthor(int id);
	}

}
