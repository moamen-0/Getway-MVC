using Auth.DEPI.Final.PL.ViewModels.HomeViewModels;
using AutoMapper;
using Auth.DEPI.Final.DAL.Entities;

namespace Auth.DEPI.Final.PL.Mapping.InstructorMapping
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor,InstructorsViewModel>().ReverseMap();
        }
    }
}
