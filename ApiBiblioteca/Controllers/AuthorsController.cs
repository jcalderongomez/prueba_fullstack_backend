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
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepositorio _authorsRepositorio;
        protected ResponseDto _response;

        public AuthorsController(IAuthorsRepositorio authorsRepositorio)
        {
            _authorsRepositorio = authorsRepositorio;
            _response = new ResponseDto();
        }

        //Get: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authors>>> GetAuthor()
        {
            try
            {
                var lista = await _authorsRepositorio.GetAuthors();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Authors";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        //Get: api/Authors

        [HttpGet("{id}")]
        public async Task<ActionResult<Authors>> GetAuthor(int id)
        {
            var author = await _authorsRepositorio.GetAuthorById(id);
            if (author == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Author Dont exists";
                return NotFound(_response);
            }
            _response.Result = author;
            _response.DisplayMessage = "información del autor";
            return Ok(_response);
        }
        [HttpPost]
        [Route("createAuthors")]
        public async Task<ActionResult<bool>> createAuthors(List<Authors> autors)
        {
            var resultCreation = await _authorsRepositorio.CreateAuthors(autors);
            if (resultCreation == false)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                return NotFound(_response);
            }
            _response.Result = resultCreation;
            _response.DisplayMessage = "Actualización de Autores completa";
            return Ok(_response);
        }
        //Put

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorsDto authorDto)
        {
            try
            {
                AuthorsDto model = await _authorsRepositorio.CreateUpdate(authorDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Authors>> Postauthor(AuthorsDto authorDto)
        {
            try
            {
                AuthorsDto model = await _authorsRepositorio.CreateUpdate(authorDto);
                _response.Result = model;
                _response.DisplayMessage = "Agregado con éxito";
                return CreatedAtAction("GetAuthor", new { id = model.id }, _response);
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
            bool estaEliminado = await _authorsRepositorio.DeleteAuthor(id);
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
