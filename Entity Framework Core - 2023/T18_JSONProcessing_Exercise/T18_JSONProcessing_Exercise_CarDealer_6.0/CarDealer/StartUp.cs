using System.IO;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new();

            //P09
            string suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            Console.WriteLine(ImportSuppliers(context, suppliersJson));

        }

        //P09
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cnf => cnf.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);

            SupplierDTO[] supplierDTOs = JsonConvert.DeserializeObject<SupplierDTO[]>(inputJson);

            Supplier[] suppliers = mapper.Map<Supplier[]>(supplierDTOs);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}.";
        }
    }
}