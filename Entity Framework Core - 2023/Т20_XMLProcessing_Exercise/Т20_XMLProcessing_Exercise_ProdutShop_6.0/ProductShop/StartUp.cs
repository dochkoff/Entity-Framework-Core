using System.Linq;
using System.Xml.Serialization;
using AutoMapper;
using ProductShop.Data;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new();

            //P01
            //string inputUsersXml = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(context, inputUsersXml));

            //P02
            //string inputProductsXml = File.ReadAllText("../../../Datasets/products.xml");
            //Console.WriteLine(ImportProducts(context, inputProductsXml));

            //P03
            string inputCategoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            Console.WriteLine(ImportCategories(context, inputCategoriesXml));
        }

        private static Mapper GetMapper()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<ProductShopProfile>());
            return new Mapper(cfg);
        }

        //P01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new(typeof(ImportUserDTO[]), new XmlRootAttribute("Users"));

            using StringReader reader = new(inputXml);
            ImportUserDTO[] importUserDTOs = (ImportUserDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();

            User[] users = mapper.Map<User[]>(importUserDTOs);

            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //P02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new(typeof(ImportProductDTO[]), new XmlRootAttribute("Products"));

            using StringReader reader = new(inputXml);
            ImportProductDTO[] importProductsDTOs = (ImportProductDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();

            Product[] products = mapper.Map<Product[]>(importProductsDTOs
                .Where(p => p.Name != null
                    && p.BuyerId != null
                    && p.SellerId != null));

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //P03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new(typeof(ImportCategoryDTO[]), new XmlRootAttribute("Categories"));

            using StringReader reader = new(inputXml);
            ImportCategoryDTO[] ImportCategoryDTOs = (ImportCategoryDTO[])xmlSerializer.Deserialize(reader);

            var mapper = GetMapper();

            Category[] categories = mapper.Map<Category[]>(ImportCategoryDTOs
                .Where(p => p.Name != null));

            context.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }
    }
}