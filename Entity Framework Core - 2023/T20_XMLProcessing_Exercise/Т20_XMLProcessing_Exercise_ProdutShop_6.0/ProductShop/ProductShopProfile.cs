﻿using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //P01
            CreateMap<ImportUserDTO, User>();

            //P02
            CreateMap<ImportProductDTO, Product>();

            //P03
            CreateMap<ImportCategoryDTO, Category>();

            //P04
            CreateMap<ImportCategoryProductsDTO, CategoryProduct>();

            //P05
            CreateMap<Product, ExportProductsInRangeDTO>();
            CreateMap<ExportProductsInRangeDTO, ExportProductsInRangeDTO>();

            //P06
            CreateMap<User, ExportUserDTO>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Product, ExportSoldProductsDTO>();

            //P07
            CreateMap<Category, ExportCategoriesDTO>();

            //P08
            CreateMap<User, ExportUsersWithCount>();
        }
    }
}
