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
            string inputSuppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            Console.WriteLine(ImportSuppliers(context, inputSuppliersXml));
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
    }
}