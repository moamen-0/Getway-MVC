using Auth.DEPI.Final.PL.ViewModels.HomeViewModels;
using AutoMapper;
using Auth.DEPI.Final.DAL.Entities;
using Auth.DEPI.Final.PL.ViewModels.InstructorViewModels;

namespace Auth.DEPI.Final.PL.Mapping.CoursesMapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Courses, CoursesViewModel>().ReverseMap();
            CreateMap<Courses, EditDeleteCourseViewModel>().ReverseMap();
        }
    }
}
