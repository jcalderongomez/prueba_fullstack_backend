using AutoMapper;
using ApiBiblioteca.Models;
using ApiBiblioteca.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<BooksDto, Books>();
                config.CreateMap<Books, BooksDto>();

                config.CreateMap<AuthorsDto, Authors>();
                config.CreateMap<Authors, AuthorsDto>();
            });
            return mappingConfig;
        }

    }
}
