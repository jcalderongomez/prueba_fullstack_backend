﻿using ApiBiblioteca.Data;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiBiblioteca.Repositorio
{
    public class UsersRepositorio : IUsersRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;


        public UsersRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;

        }
        public async Task<string> Login(string userName, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));

            if (user == null)
            {
                return "nouser";
            }
            else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return CrearToken(user);
            }
        }

        public async Task<string> Register(Users user, string passowrd)
        {
            try
            {

                if (await UserExists(user.UserName))
                {
                    return "existe";
                }

                CrearPasswordHash(passowrd, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return CrearToken(user);
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _db.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CrearToken(Users user)
        {
            var claim = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.
                                        GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

   
}
