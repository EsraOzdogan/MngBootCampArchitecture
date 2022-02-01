using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Model:Entity
    {
        public Model()
        {

        }
        public Model(int id, string name, double dailyPrice, int transmissionTypeId, int fuelTypeId, int brandId, string imageUrl):this()
        {
            Id = id;    
            Name = name;
            DailyPrice = dailyPrice;
            TransmissionId = transmissionTypeId;
            FuelId = fuelTypeId;
            BrandId = brandId;
            ImageUrl = imageUrl;
        }
        //public int Id { get; set; }

        public int TransmissionId { get; set; } //sanziman tipi
        public int FuelId { get; set; } //yakit tipi
        public int BrandId { get; set; } //yakit tipi
        public string Name { get; set; }
        public double DailyPrice { get; set; }
       
        public string ImageUrl { get; set; }

        public virtual Brand Brand { get; set; }//virtual override edilebilir olmasini gösteriyor
        public virtual Transmission Transmission { get; set; }
        public virtual Fuel Fuel { get; set; }//Modelin yakıtı 1 tane olur

        public virtual ICollection<Car> Cars { get; set; }
    }


}
