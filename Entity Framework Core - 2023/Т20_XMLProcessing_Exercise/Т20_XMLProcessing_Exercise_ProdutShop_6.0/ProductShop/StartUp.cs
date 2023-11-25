using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.DTOs.Export;
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
            //string inputCategoriesXml = File.ReadAllText("../../../Datasets/categories.xml");
            //Console.WriteLine(ImportCategories(context, inputCategoriesXml));

            //P04
            //string inputCategoryProductsXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, inputCategoryProductsXml));

            //P05
            Console.WriteLine(GetProductsInRange(context));
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

        //P04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer xmlSerializer = new(typeof(ImportCategoryProductsDTO[]), new XmlRootAttribute("CategoryProducts"));

            using StringReader reader = new(inputXml);
            ImportCategoryProductsDTO[] importCategoryProductsDTOs = (ImportCategoryProductsDTO[])xmlSerializer.Deserialize(reader);

            var categoriesId = context.Categories
                .Select(c => c.Id)
                .ToArray();

            var productsId = context.Products
                .Select(p => p.Id)
                .ToArray();

            var mapper = GetMapper();

            CategoryProduct[] categoryProducts = mapper.Map<CategoryProduct[]>(importCategoryProductsDTOs
                .Where(cp =>
                    categoriesId.Contains(cp.CategoryId)
                    && productsId.Contains(cp.ProductId)));

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var mapper = GetMapper();

            ExportProductsInRangeDTO[] products = context
            .Products
            .Where(p => p.Price >= 500 && p.Price <= 1000)
            .Select(p => new ExportProductsInRangeDTO()
            {
                Name = p.Name,
                Price = p.Price,
                BuyerName = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
            })
            .OrderBy(p => p.Price)
            .Take(10)
            .ProjectTo<ExportProductsInRangeDTO>(mapper.ConfigurationProvider)
            .ToArray();



            return SerializeToXml<ExportProductsInRangeDTO[]>(products, "Products");
        }

        private static string SerializeToXml<T>(T dto, string xmlRootAttribute)
        {
            XmlSerializer xmlSerializer =
                new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttribute));

            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add(string.Empty, string.Empty);

                try
                {
                    xmlSerializer.Serialize(stringWriter, dto, xmlSerializerNamespaces);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return stringBuilder.ToString();
        }
    }
}