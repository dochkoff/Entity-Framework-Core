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
            //string suppliersJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(context, suppliersJson));

            //P10
            //string partsJson = File.ReadAllText("../../../Datasets/parts.json");
            //Console.WriteLine(ImportParts(context, partsJson));

            //P11
            string carsJson = File.ReadAllText("../../../Datasets/cars.json");
            Console.WriteLine(ImportCars(context, carsJson));

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

        //P10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var config = new MapperConfiguration(cnf => cnf.AddProfile<CarDealerProfile>());
            IMapper mapper = new Mapper(config);

            PartsDTO[] partsDTOs = JsonConvert.DeserializeObject<PartsDTO[]>(inputJson);
            Part[] parts = mapper.Map<Part[]>(partsDTOs);

            int[] supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();

            Part[] partsWithValidSuppliers = parts
                .Where(p => supplierIds
                .Contains(p.SupplierId))
                .ToArray();

            context.Parts.AddRange(partsWithValidSuppliers);
            context.SaveChanges();

            return $"Successfully imported {partsWithValidSuppliers.Count()}.";
        }

        //P11
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars = JsonConvert.DeserializeObject<Car[]>(inputJson);

            if (cars != null)
            {
                context.Cars.AddRange(cars);
                context.SaveChanges();
            }

            return $"Successfully imported {cars?.Length}.";
        }
    }
}