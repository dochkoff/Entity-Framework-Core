using System.Globalization;
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
            //string carsJson = File.ReadAllText("../../../Datasets/cars.json");
            //Console.WriteLine(ImportCars(context, carsJson));

            //P12
            //string customersJson = File.ReadAllText("../../../Datasets/customers.json");
            //Console.WriteLine(ImportCustomers(context, customersJson));

            //P13
            //string salesJson = File.ReadAllText("../../../Datasets/sales.json");
            //Console.WriteLine(ImportSales(context, salesJson));

            //P14
            //Console.WriteLine(GetOrderedCustomers(context));

            //P15
            //Console.WriteLine(GetCarsFromMakeToyota(context));

            //P16
            //Console.WriteLine(GetLocalSuppliers(context));

            //P17
            Console.WriteLine(GetCarsWithTheirListOfParts(context));

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

        //P12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson);


            if (customers != null)
            {
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            return $"Successfully imported {customers?.Length}.";
        }

        //P13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson);


            if (sales != null)
            {
                context.Sales.AddRange(sales);
                context.SaveChanges();
            }

            return $"Successfully imported {sales?.Length}.";
        }

        //P14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var orderedCustomers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);
            return jsonOutput;
        }

        //P15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var carsMadeByToyota = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TraveledDistance,
                })
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(carsMadeByToyota, Formatting.Indented);
            return jsonOutput;
        }

        //P16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToArray();

            string jsonOutput = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);
            return jsonOutput;
        }

        //P17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carWithPartsList = context.Cars
                .Select(c => new
                {
                    c.Make,
                    c.Model,
                    c.TraveledDistance,
                    Parts = c.PartsCars
                    .Select(p => new
                    {
                        p.Part.Name,
                        Price = p.Part.Price.ToString("F2")
                    })
                    .ToList()
                })
                .ToArray();

            string jsonOtput = JsonConvert.SerializeObject(carWithPartsList
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance,
                    },
                    parts = c.Parts
                }), Formatting.Indented);
            return jsonOtput;
        }
    }
}