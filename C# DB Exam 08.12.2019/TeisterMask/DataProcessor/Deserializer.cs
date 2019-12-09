namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Xml.Serialization;
    using System.IO;
    using TeisterMask.Data.Models;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using TeisterMask.Data.Models.Enums;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectDto[]), new XmlRootAttribute("Projects"));

            var projectsDto = (ImportProjectDto[])xmlSerializer.Deserialize(new StringReader(xmlString));

            List<Project> projects = new List<Project>();

            StringBuilder sb = new StringBuilder();

            foreach (var dto in projectsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                List<Task> tasks = new List<Task>();

                Project project = new Project
                {
                    Name = dto.Name,
                    OpenDate = DateTime.ParseExact(dto.OpenDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture),
                    DueDate = dto.DueDate,
                };

                if (!IsValid(project))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var dtoTask in dto.Tasks)
                {
                    Task task = new Task();

                    task.Name = dtoTask.Name;
                    task.OpenDate = DateTime.ParseExact(dtoTask.OpenDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture);
                    task.DueDate = DateTime.ParseExact(dtoTask.DueDate, @"dd/MM/yyyy", CultureInfo.InvariantCulture);
                    task.ExecutionType = (ExecutionType)Enum.Parse(typeof(ExecutionType), dtoTask.ExecutionType);
                    task.LabelType = (LabelType)Enum.Parse(typeof(LabelType), dtoTask.LabelType);
                    task.ProjectId = project.Id;

                    if (!IsValid(task))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    tasks.Add(task);

                }

                project.Tasks = tasks;



                if (IsValid(project))
                {
                    projects.Add(project);
                    sb.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Projects.AddRange(projects);
            context.SaveChanges();


            return sb.ToString().TrimEnd();

        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var dtos = JsonConvert.DeserializeObject<ImportEmployeesDto[]>(jsonString);


            StringBuilder sb = new StringBuilder();

            List<Employee> employees = new List<Employee>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee
                {
                    Username = dto.Username,
                    Phone = dto.Phone,
                    Email = dto.Email
                };




                var dtoTasksDistincted = dto.Tasks.Distinct();

                foreach (var currentTask in dtoTasksDistincted)
                {
                    bool isExist = context.Tasks.Any(x => x.Id == currentTask);

                    if (!isExist)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }


                    EmployeeTask employeeTask = new EmployeeTask
                    {
                        TaskId = currentTask
                    };

                    employee.EmployeesTasks.Add(employeeTask);
                }


                context.SaveChanges();
                employees.Add(employee);
                sb.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username,
                        employee.EmployeesTasks.Count));

            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}