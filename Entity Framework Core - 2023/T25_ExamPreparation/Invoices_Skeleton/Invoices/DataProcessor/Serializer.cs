namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ExportDto;
    using Invoices.DataProcessor.ImportDto;
    using Invoices.Extensions;
    using Microsoft.EntityFrameworkCore;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            var clients = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate > date))
                .Select(c => new ExportClientsWithInvoicesDto()
                {
                    InvoicesCount = c.Invoices.Count,
                    ClientName = c.Name,
                    VatNumber = c.NumberVat,
                    Invoices = c.Invoices
                        .OrderBy(i => i.IssueDate)
                        .ThenByDescending(i => i.DueDate)
                        .Select(i => new ExportInvoicesDto()
                        {
                            InvoiceNumber = i.Number,
                            InvoiceAmount = (double)i.Amount,
                            DueDate = i.DueDate.ToString("MM/dd/yyyy"),
                            Currency = i.CurrencyType
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.Invoices.Count())
                .ThenBy(c => c.ClientName)
            .ToArray();

            return clients.SerializeToXml<ExportClientsWithInvoicesDto[]>("Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Include(p => p.ProductsClients)
                .ThenInclude(pc => pc.Client)
                .Select(p => new ExportProductsMostClientsDto()
                {
                    Name = p.Name,
                    Price = (double)p.Price,
                    Category = p.CategoryType,
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .Select(pc => new ExportClientDto()
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .OrderBy(c => c.Name)
                        .ToArray()
                })
                .OrderByDescending(ep => ep.Clients.Count())
                .ThenBy(ep => ep.Name)
                .Take(5)
                .ToArray();

            return products.SerializeToJson<ExportProductsMostClientsDto[]>();

        }
    }
}