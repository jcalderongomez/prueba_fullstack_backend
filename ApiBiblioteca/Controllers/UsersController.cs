using ApiBiblioteca.Models;
using ApiBiblioteca.Models.Dto;
using ApiBiblioteca.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepositorio _userRepostorio;
        protected ResponseDto _response;

        public UsersController(IUsersRepositorio userRepositorio)
        {
            _userRepostorio = userRepositorio;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UsersDto user)
        {
            var respuesta = await _userRepostorio.Register(
                new Users
                {
                    UserName = user.UserName
                }, user.Password);
            if (respuesta == "existe")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usario ya existe";
                return BadRequest(_response);
            }

            if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario creado con éxito";
            //_response.Result = respuesta;
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;

            return Ok(_response);

        }

        
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsersDto user)
        {
            var respuesta = await _userRepostorio.Login(user.UserName, user.Password);

            if (respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usario no existe";
                return BadRequest(_response);
            }

            if (respuesta == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Clave incorrecta";
                return BadRequest(_response);
            }
            //_response.Result = respuesta;
            JwTPackage jtp = new JwTPackage();
            jtp.UserName = user.UserName;
            jtp.Token = respuesta;
            _response.Result = jtp;

            _response.DisplayMessage = "usuario conectado";
            return Ok(_response);
        }
    }

    public class JwTPackage {
        public string UserName { get; set; }
        public string Token { get; set; }
    }

}
