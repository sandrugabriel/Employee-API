using AutoMapper;
using EmployeeAPI.Dto;
using EmployeeAPI.Models;

namespace EmployeeAPI.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {


            CreateMap<CreateRequest, Employee>();
        }


    }
}
