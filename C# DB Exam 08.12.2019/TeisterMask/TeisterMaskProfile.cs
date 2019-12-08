using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TeisterMask.Data.Models;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask
{
    public class TeisterMaskProfile : Profile
    {
        public TeisterMaskProfile()
        {
            this.CreateMap<ImportProjectDto, Project>()
                .ForMember(x => x.OpenDate, y => y.MapFrom(x => DateTime.ParseExact(x.OpenDate, @"dd/MM/yyyy",
                CultureInfo.InvariantCulture)))
                .ForMember(x => x.DueDate, y => y.MapFrom(x => DateTime.ParseExact(x.DueDate, @"dd/MM/yyyy",
                CultureInfo.InvariantCulture)));

            this.CreateMap<ImportTaskDto, Task>();



        }
    }
}
