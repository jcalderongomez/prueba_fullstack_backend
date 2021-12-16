using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBiblioteca.Models;

namespace ApiBiblioteca.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
