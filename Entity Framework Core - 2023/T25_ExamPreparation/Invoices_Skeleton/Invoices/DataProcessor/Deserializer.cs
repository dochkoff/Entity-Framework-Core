namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ImportDto;
    using Invoices.Extensions;
    using Microsoft.EntityFrameworkCore;
    using System.Data;
    using Newtonsoft.Json;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new();

            List<ImportClientDto> clientsDto = xmlString
                .DeserializeFromXml<List<ImportClientDto>>("Clients");

            List<Client> clients = new();

            foreach (var client in clientsDto)
            {
                if (!IsValid(client))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var clientToAdd = new Client
                {
                    Name = client.Name,
                    NumberVat = client.NumberVat
                };

                foreach (var address in client.Addresses)
                {
                    if (IsValid(address))
                    {
                        clientToAdd.Addresses.Add(new Address()
                        {
                            City = address.City,
                            Country = address.Country,
                            PostCode = address.PostCode,
                            StreetName = address.StreetName,
                            StreetNumber = address.StreetNumber
                        });
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                }

                clients.Add(clientToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedClients, client.Name));
            }

            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new();

            List<ImportInvoiceDto> invoicesDto = jsonString
                .DeserializeFromJson<List<ImportInvoiceDto>>();

            List<Invoice> invoices = new();

            int[] clientsIds = context.Clients.Select(c => c.Id).ToArray();

            foreach (var invoice in invoicesDto)
            {
                if (!IsValid(invoice) || !clientsIds.Contains(invoice.ClientId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime issueDate = DateTime.ParseExact(invoice.IssueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                DateTime dueDate = DateTime.ParseExact(invoice.DueDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                if (dueDate < issueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var invoiceToAdd = new Invoice
                {
                    Number = invoice.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    ClientId = invoice.ClientId,
                    Amount = invoice.Amount,
                    CurrencyType = invoice.CurrencyType
                };

                invoices.Add(invoiceToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedInvoices, invoice.Number));
            }

            context.Invoices.AddRange(invoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new();

            List<ImportProductDto> productDtos = jsonString
                .DeserializeFromJson<List<ImportProductDto>>();

            List<Product> products = new();

            int[] clientIds = context.Clients
                .AsNoTracking()
                .Select(x => x.Id)
                .ToArray();

            foreach (var product in productDtos)
            {
                if (!IsValid(product) || product.Price < 5.00m || product.Price > 1000.00m)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var productToAdd = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryType = product.CategoryType
                };

                foreach (var clientId in product.Clients.Distinct())
                {
                    if (clientIds.Contains(clientId))
                    {
                        productToAdd.ProductsClients.Add(new ProductClient
                        {
                            ClientId = clientId
                        });
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                }

                products.Add(productToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedProducts, product.Name, productToAdd.ProductsClients.Count()));
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
