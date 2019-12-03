using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();
            this.CreateMap<ImportPartDto, Part>();


            this.CreateMap<Part, ExportPartsDto>();
            this.CreateMap<Car, ExportCarsWithParts>()
                .ForMember(x => x.Parts, y => y.MapFrom(x => x.PartCars.Select(p => p.Part)));
            this.CreateMap<Supplier, ExportLocalSuppliersDto>();
        }
    }
}
