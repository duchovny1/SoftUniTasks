using AutoMapper;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<DtoUser, User>();
            this.CreateMap<DtoProduct, Product>();
            this.CreateMap<DtoCategories, Category>();
            this.CreateMap<DtoCategoryProducts, CategoryProduct>();
        }
    }
}
