﻿using AutoMapper;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<DTO_ImportSuppliers, Supplier>();
            this.CreateMap<DTO_ImportCars, Car>();
        }
    }
}
