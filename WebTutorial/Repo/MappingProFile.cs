using AutoMapper;
using WebTutorial.Models;
using WebTutorial.Models.DTO;

namespace WebTutorial.Repo
{
    public class MappingProFile : Profile
    {
        public MappingProFile() 
        { 
            CreateMap<Product, ProbuctDto>(MemberList.Destination).ReverseMap();
            CreateMap<Category, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<Storage, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
