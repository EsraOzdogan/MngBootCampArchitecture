using Application.Features.Brands.Commands.CreateBrand;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles:Profile //AutoMapper'dan geldi
    {
        public MappingProfiles()
        {
            CreateMap< Brand, CreateBrandCommand>().ReverseMap(); //Brands, CreateBrandCommand bu ters oldugu icin ReverseMap kullandik

        }
    }
}
