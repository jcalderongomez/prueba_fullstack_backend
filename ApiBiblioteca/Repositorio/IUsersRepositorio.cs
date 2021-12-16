using ApiBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Repositorio
{
    public interface IUsersRepositorio
    {
        Task<string> Register(Users user, string passowrd);

        Task<string> Login(string userName, string passowrd);

        Task<bool> UserExists(string username);

    }
}
