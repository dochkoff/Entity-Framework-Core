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
            string inputPartsXml = File.ReadAllText("../../../Datasets/parts.xml");
            Console.WriteLine(ImportParts(context, inputPartsXml));
        }

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<CarDealerProfile>());
            return new Mapper(cfg);
        }

        //P09
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            //1.Create XML serializer
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSuplierDTO[]), new XmlRootAttribute("Suppliers"));

            //2.
            using var reader = new StringReader(inputXml);
            ImportSuplierDTO[] importSuplierDTOs = (ImportSuplierDTO[])xmlSerializer.Deserialize(reader);

            //3.
            var mapper = GetMapper();
            Supplier[] suppliers = mapper.Map<Supplier[]>(importSuplierDTOs);

            //4. Add to EF context
            context.AddRange(suppliers);

            //5 Commut DB changes
            context.SaveChanges();

            return $"Successfully imported {suppliers.Length}";
        }

        //P10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartDTO[]), new XmlRootAttribute("Parts"));

            using var reader = new StringReader(inputXml);
            ImportPartDTO[] importPartDTOs = (ImportPartDTO[])xmlSerializer.Deserialize(reader);

            var suppliersIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();

            var mapper = GetMapper();
            Part[] parts = mapper.Map<Part[]>(importPartDTOs.Where(x => suppliersIds.Contains(x.SupplierId)));

            context.AddRange(parts);

            context.SaveChanges();

            return $"Successfully imported {parts.Length}";
        }
    }
}