using AutoMapper;
using Core.Entities;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services.Mappings
{

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
              
                CreateMap<CityCoordinate, CityCoordinateDto>();

                CreateMap<CityCoordinateDto, CityCoordinate>();
            }
        }
    
}
