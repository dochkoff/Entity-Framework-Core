using System.Text;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            //Console.WriteLine(GetEmployeesFullInformation(context));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
            //Console.WriteLine(AddNewAddressToEmployee(context));
            Console.WriteLine(GetEmployeesInPeriod(context));

            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
        }

        //P03
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}"));
            return result;
        }

        //P04
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .ToList();

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} - {e.Salary:F2}"));

            return result;
        }

        //P05
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:F2}"));

            return result;
        }

        //P06
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            var employees = context.Employees
                .Select(e => new
                {
                    e.AddressId,
                    e.Address.AddressText
                })
                .OrderByDescending(e => e.AddressId)
                .Take(10);

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.AddressText}"));

            return result;
        }

        //P07
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var dataString = new StringBuilder();

            var employees = context.Employees
                .Take(10)
                .Include(e => e.Manager)
                .Include(e => e.EmployeesProjects)
                .ToArray();

            for (int i = 0; i < 10; i++)
            {
                var e = employees[i];

                dataString.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.Manager?.FirstName} {e.Manager.LastName}");

                if (e.EmployeesProjects.Count > 0)
                {
                    var projects = context.EmployeesProjects
                        .Select(ep => new
                        {
                            ep.EmployeeId,
                            ep.Project.Name,
                            ep.Project.StartDate,
                            ep.Project.EndDate,
                        })
                        .Where(ep => ep.EmployeeId == e.EmployeeId)
                        .ToArray();

                    foreach (var p in projects)
                    {
                        if (p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
                        {
                            var enddate = p.EndDate != null ? p.EndDate?.ToString("M/d/yyyy h:mm:ss tt") : "not finished";

                            dataString.AppendLine($"--{p.Name} - {p.StartDate.ToString("M/d/yyyy h:mm:ss tt")} - {enddate}");
                        }
                    }
                }
            }

            return dataString.ToString().Trim();
        }

        //P13
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            string result = string.Join(Environment.NewLine, employees.Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})"));

            return result;
        }
    }
}