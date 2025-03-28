using AutoMapper;
using Company.G02.DAL.Models;
using Company.G02.PL.DTO;

namespace Company.G02.PL.Mapping
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDTO, Employee>().ReverseMap();
         
        }

    }
}
