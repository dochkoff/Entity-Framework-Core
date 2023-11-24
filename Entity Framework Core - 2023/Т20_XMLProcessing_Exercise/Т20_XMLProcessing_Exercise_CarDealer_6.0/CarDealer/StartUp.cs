using System.Data;
using System.Xml.Serialization;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            //P09
            //string inputSuppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            //Console.WriteLine(ImportSuppliers(context, inputSuppliersXml));

            //P10
            //string inputPartsXml = File.ReadAllText("../../../Datasets/parts.xml");
            //Console.WriteLine(ImportParts(context, inputPartsXml));

            //P11
            string inputCarsXml = File.ReadAllText("../../../Datasets/cars.xml");
            Console.WriteLine(ImportCars(context, inputCarsXml));

        }

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }

        //P09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            // 1. Create xml serializer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSupplierDTO[]),
                new XmlRootAttribute("Suppliers"));

            // 2. 
            using var reader = new StringReader(inputXml);
            ImportSupplierDTO[] importSupplierDTOs = (ImportSupplierDTO[])xmlSerializer.Deserialize(reader);

            // 3. 
            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSupplierDTOs);

            // 4. Add to EF contexrt
            context.AddRange(suppliers);

            // 5. Commit chages to DB
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        //P10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportPartsDTO[]), new XmlRootAttribute("Parts"));

            using StringReader inputReader = new StringReader(inputXml);
            ImportPartsDTO[] importPartsDTOs = (ImportPartsDTO[])xmlSerializer.Deserialize(inputReader);

            var supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();

            var mapper = GetMapper();

            Part[] parts = mapper.Map<Part[]>(importPartsDTOs
                .Where(p => supplierIds.Contains(p.SupplierId)));

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }

        //P11
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(ImportCarDTO[]), new XmlRootAttribute("Cars"));

            using StringReader stringReader = new StringReader(inputXml);

            ImportCarDTO[] importCarDTOs = (ImportCarDTO[])xmlSerializer.Deserialize(stringReader);

            var mapper = GetMapper();
            List<Car> cars = new List<Car>();

            foreach (var carDTO in importCarDTOs)
            {
                Car car = mapper.Map<Car>(carDTO);

                int[] carPartIds = carDTO.PartsIds
                    .Select(x => x.Id)
                    .Distinct()
                    .ToArray();

                var carParts = new List<PartCar>();

                foreach (var id in carPartIds)
                {
                    carParts.Add(new PartCar
                    {
                        Car = car,
                        PartId = id
                    });
                }

                car.PartsCars = carParts;
                cars.Add(car);
            }

            context.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";

        }



    }
}