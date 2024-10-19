using Auth.DEPI.Final.DAL.Entities;
using Auth.DEPI.Final.PL.ViewModels.InstructorViewModels;
using AutoMapper;

namespace Auth.DEPI.Final.PL.Mapping.VideoMapping
{
    public class VideoProfile : Profile
    {
        public VideoProfile() { 
        CreateMap<Video,VideoViewModel>().ReverseMap();
        }
    }
}
