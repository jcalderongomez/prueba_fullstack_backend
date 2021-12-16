using ApiBiblioteca.Repositorio;
using ApiBiblioteca.Data;
using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBiblioteca.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepositorio _booksRepositorio;
        protected ResponseDto _response;

        public BooksController(IBooksRepositorio BooksRepositorio)
        {
            _booksRepositorio = BooksRepositorio;
            _response = new ResponseDto();
        }

        //Get: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBook()
        {
            try
            {
                var lista = await _booksRepositorio.GetBooks();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de registros";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get: api/Books

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            var author = await _booksRepositorio.GetBookById(id);
            if (author == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Registro no existe";
                return NotFound(_response);
            }
            _response.Result = author;
            _response.DisplayMessage = "información del autor";
            return Ok(_response);
        }
        [HttpPost]
        [Route("GetBookAuth")]
        public async Task<ActionResult<Books>> GetBookAuth(FindBook findbooks)
        {
            var author = await _booksRepositorio.GetBooksAuthById(findbooks);
            if (author == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No existe";
                return NotFound(_response);
            }
            _response.Result = author;
            _response.DisplayMessage = "información del autor";
            return Ok(_response);
        }
        [HttpPost]
        [Route("createBooks")]
        public async Task<ActionResult<bool>> createBooks(List<Books> books)
        {
            var resultCreation = await _booksRepositorio.CreateBooks(books);
            if (resultCreation == false)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                return NotFound(_response);
            }
            _response.Result = resultCreation;
            _response.DisplayMessage = "Actualización de Libros completa";
            return Ok(_response);
        }


        //Put

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BooksDto authorDto)
        {
            try
            {
                BooksDto model = await _booksRepositorio.CreateUpdate(authorDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el autor";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Books>> Postauthor(BooksDto authorDto)
        {
            try
            {
                BooksDto model = await _booksRepositorio.CreateUpdate(authorDto);
                _response.Result = model;
                return CreatedAtAction("GetBook", new { id = model.id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al grabar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteauthor(int id)
        {
            bool estaEliminado = await _booksRepositorio.DeleteBook(id);
            try
            {
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Registro Eliminado con éxito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar el registro";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }
    }
}