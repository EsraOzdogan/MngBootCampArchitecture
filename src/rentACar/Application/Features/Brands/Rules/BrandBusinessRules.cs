using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.CrossCuttingConcerns.Exceptions.BusinessExceptions;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        //veri tabanina bakip daha önce bir brand eklenmis mi dye bakmamiz lazim. O yüzden bir injection yazıyoruz
        IBrandRepository _brandRepository;
        public BrandBusinessRules(IBrandRepository brandReporsitory)
        {
            _brandRepository = brandReporsitory;

        }

        //Gerkhin
        public async Task BrandNameCanNotBeDublicatedWhenInserted(string name)
        {
            var result = await _brandRepository.GetListAsync(b=>b.Name == name);
            if(result.Items.Any())
            {
                throw new BusinessException("Brand name exists");  //BusinessException is kurallarina karsilik gelecek
            }
        }
    }
}
