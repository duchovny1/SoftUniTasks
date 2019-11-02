
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = GetDepartmentsWithMoreThan5Employees(context);

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
            Address address = new Address
            {

                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);

            var employeeNakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            employeeNakov.Address = address;
            context.SaveChanges();


            StringBuilder sb = new StringBuilder();

            var employees = context.Employees.OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10).ToList();

            foreach (var item in employees)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employeesProjects = context.Employees.Where(e => e.EmployeesProjects
            .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
            .Select(e => new
            {
                EmployeeName = e.FirstName + " " + e.LastName,
                ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                Projects = e.EmployeesProjects
                .Select(p => new
                {
                    ProjectName = p.Project.Name,
                    StartDate = p.Project.StartDate,
                    EndDate = p.Project.EndDate
                })
                .ToList()
            })
            .Take(10)
            .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employeesProjects)
            {
                sb.AppendLine($"{e.EmployeeName} - Manager: {e.ManagerName}");

                foreach (var project in e.Projects)
                {
                    var startDate = project.StartDate
                        .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    var endDate = project.EndDate == null ? "not finished" : project.EndDate.Value
                        .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");

                }
            }

            return sb.ToString().TrimEnd();


        }

        public static string GetAddressesByTown(SoftUniContext context)
        {


            var addresses = context.Addresses.
                Select(e => new
                {
                    e.AddressText,
                    e.Town.Name,
                    e.Employees.Count
                }).OrderByDescending(e => e.Count).ThenBy(e => e.Name).ThenBy(e => e.AddressText)
            .Take(10).ToList();



            StringBuilder sb = new StringBuilder();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.Name} - {a.Count} employees");
            }

            return sb.ToString();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee147 = context.Employees.Where(x => x.EmployeeId == 147)
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects.Select(p => new
                {
                    p.Project.Name
                }).OrderBy(p => p.Name).ToList()
            }).FirstOrDefault();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");


            foreach (var p in employee147.Projects)
            {
                sb.AppendLine(p.Name + " ");
            }


            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count() > 5)
                .OrderBy(d => d.Employees.Count())
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees
                    .Select(e => new
                    {
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        JobTitile = e.JobTitle
                    })
                    .OrderBy(e => e.EmployeeFirstName)
                    .ThenBy(e => e.EmployeeLastName)
                    .ToList()
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - " +
                    $"{department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - " +
                        $"{employee.JobTitile}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
