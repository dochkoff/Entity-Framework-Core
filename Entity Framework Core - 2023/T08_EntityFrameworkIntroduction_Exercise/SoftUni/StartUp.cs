﻿using System.Globalization;
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
            //Console.WriteLine(GetEmployeesInPeriod(context));
            //Console.WriteLine(GetAddressesByTown(context));
            //Console.WriteLine(GetEmployee147(context));
            //Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
            //Console.WriteLine(GetLatestProjects(context));
            //Console.WriteLine(IncreaseSalaries(context));
            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
            //Console.WriteLine(DeleteProjectById(context));
            Console.WriteLine(RemoveTown(context));
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

        //P08
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    a.Town.Name,
                    a.Employees
                })
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Name)
                .Take(10)
                .ToList();

            string result = string.Join(Environment.NewLine, addresses.Select(a => $"{a.AddressText}, {a.Name} - {a.Employees.Count} employees"));

            return result;
        }

        //P09
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new();

            var employee = context.Employees
                .FirstOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            var projects = context.EmployeesProjects
                .Select(ep => new
                {
                    ep.EmployeeId,
                    ep.Project.Name
                })
                .Where(ep => ep.EmployeeId == 147)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Name}");
            }

            return sb.ToString();

        }

        //P10
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new();

            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    d.Name,
                    d.Manager,
                    d.Employees
                })
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}");

                var employeesInDepartment = d.Employees
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();

                foreach (var e in employeesInDepartment)
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString();
        }

        //P11
        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderBy(p => p.Name)
                .ToList();

            StringBuilder sb = new();

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Name}");
                sb.AppendLine($"{p.Description}");
                sb.AppendLine($"{p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
            }

            return sb.ToString();
        }

        //P12
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);

            StringBuilder sb = new();

            foreach (var e in employees)
            {
                e.Salary *= 1.12M;
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }

            return sb.ToString();
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

        //P14
        public static string DeleteProjectById(SoftUniContext context)
        {
            var project = context.Projects
                .Find(2);

            context.EmployeesProjects.RemoveRange(context.EmployeesProjects.Where(ep => ep.Project == project));
            context.Projects.Remove(project);

            context.SaveChanges();

            var result = new StringBuilder();

            var projects = context.Projects
                .Take(10)
                .ToList();

            foreach (var p in projects)
            {
                result.AppendLine(p.Name);
            }

            return result.ToString().Trim();
        }

        //P15
        public static string RemoveTown(SoftUniContext context)
        {
            var town = context.Towns
                .Include(t => t.Addresses)
                .FirstOrDefault(t => t.Name == "Seattle");

            context.Addresses.RemoveRange(town.Addresses);
            context.Towns.Remove(town);

            var employees = context.Employees
                .Where(e => e.Address.Town.Name == "Seattle");

            foreach (var e in employees)
            {
                e.AddressId = null;
            }

            context.SaveChanges();

            return $"{town.Addresses.Count} addresses in Seattle were deleted";
        }
    }
}