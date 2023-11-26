using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSupplierDTO, Supplier>();

            CreateMap<ImportPartsDTO, Part>();

            CreateMap<ImportCarDTO, Car>();

            CreateMap<ImportCustomerDTO, Customer>();

            CreateMap<ImportSaleDTO, Sale>();

            CreateMap<Car, ExportCarWithAttrDTO>();

            CreateMap<Car, ExportCarsFromMakeBmw>();

            CreateMap<Supplier, ExportLocalSuppliers>();
            CreateMap<ExportLocalSuppliers, ExportLocalSuppliers>();

            CreateMap<Car, ExportCarsWithTheirListOfParts>();
            CreateMap<Part, ExportPartsDTO>();
            CreateMap<ExportCarsWithTheirListOfParts, ExportCarsWithTheirListOfParts>();

            CreateMap<Sale, ExportSaleAppliedDiscountDTO>();

        }
    }
}
