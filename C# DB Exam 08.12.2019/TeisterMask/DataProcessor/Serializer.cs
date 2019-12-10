﻿namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .Where(x => x.Tasks.Count > 0)
                .Select(x => new ExportProjectDto
                {
                    TasksCount = x.Tasks.Count(),
                    ProjectName = x.Name,
                    HasEndDate = x.DueDate == null ? "No" : "Yes",
                    Tasks = x.Tasks.Select(p => new ExportTask
                    {
                        Name = p.Name,
                        Label = p.LabelType.ToString()
                    }).OrderBy(o => o.Name)
                    .ToArray()
                })
                .OrderByDescending(x => x.TasksCount)
                .ThenBy(x => x.ProjectName)
                .ToArray();


            var root = new XmlRootAttribute("Projects");

            var serializer = new XmlSerializer(typeof(ExportProjectDto[]), root);


            StringBuilder sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), projects, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {


            var emplyees = context.Employees
                .Where(e => e.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(t => t.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Select(e => new ExportEmployeesDto
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                    .Where(t => t.Task.OpenDate >= date)
                    .Select(t => new ExportTaskDto
                    {
                        TaskName = t.Task.Name,
                        LabelType = t.Task.LabelType.ToString(),
                        ExecutionType = t.Task.ExecutionType.ToString(),
                        DueDate = t.Task.DueDate.ToString(@"d", CultureInfo.InvariantCulture),
                        OpenDate = t.Task.OpenDate.ToString(@"d", CultureInfo.InvariantCulture)
                    })
                    .OrderByDescending(t => DateTime.ParseExact(t.DueDate, @"d", CultureInfo.InvariantCulture))
                    .ThenBy(t => t.TaskName)
                    .ToList()
                })
                .Take(10)
                .ToList();

            var json = JsonConvert.SerializeObject(emplyees, Formatting.Indented);

            return json;
        }
    }
}