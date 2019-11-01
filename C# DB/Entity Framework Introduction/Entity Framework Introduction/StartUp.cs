
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = AddNewAddressToEmployee(context);

            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            var employeesInfo = context.Employees.OrderBy(e => e.EmployeeId)
                .Select(e => new
                {

                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary

                })
                .ToList();

            foreach (var e in employeesInfo)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }





            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesInfo = context.Employees.OrderBy(e => e.FirstName).Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                }).
                ToList();

            foreach (var item in employeesInfo)
            {
                sb
                  .AppendLine($"{item.FirstName} - {item.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeesFromDepartment = context.Employees
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DepartmentName = e.Department.Name,
                    Salary = e.Salary
                })
                .Where(e => e.DepartmentName == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();


            foreach (var item in employeesFromDepartment)
            {
                sb.AppendLine($"{item.FirstName} {item.LastName} from {item.DepartmentName} - ${item.Salary:f2}");
            }

            return sb.ToString();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address {

                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);

            var employeeNakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            employeeNakov.Address = address;
            context.SaveChanges();


            StringBuilder sb = new StringBuilder();

            var employees = context.Employees.OrderByDescending(e => e.AddressId)
                .Select(e=> e.Address.AddressText)
                .Take(10).ToList();

            foreach (var item in employees)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
