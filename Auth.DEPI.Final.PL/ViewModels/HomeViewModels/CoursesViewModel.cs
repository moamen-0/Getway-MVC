using OnlineLearningPlatform.Models;

namespace Auth.DEPI.Final.PL.ViewModels.HomeViewModels
{
    public class CoursesViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public IFormFile CourseImage { get; set; }
        public string? Thumbnail { get; set; }
        public Instructor Instructor { get; set; }

    }
}
