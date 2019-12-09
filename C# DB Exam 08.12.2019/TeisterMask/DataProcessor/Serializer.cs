namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            throw new NotImplementedException();
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