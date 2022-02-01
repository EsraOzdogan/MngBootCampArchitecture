using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand:IRequest<Brand> //MediatR'den geldi
    {
        public string Name { get; set; }    
        public class CreateBrandCommandHandler:IRequestHandler<CreateBrandCommand,Brand>
        {
            //Gelen nesneyi veri tabani nesnesine cevirmem gerekiyor
            IBrandRepository _brandRepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;

            }
            public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                /*Brand brand= new Brand();
                brand.Name = request.Name;
                 var createBrand = await _brandRepository.AddAsync(brand);*/
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                var mappedBrand = _mapper.Map<Brand>(request);

                var createBrand = await _brandRepository.AddAsync(mappedBrand);
                return createBrand;

            }
        }
    }
}
