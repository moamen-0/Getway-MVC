using Auth.DEPI.Final.DAL.Entities;

namespace Auth.DEPI.Final.PL.ViewModels.InstructorViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string CourseId { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public IFormFile CourseVideo { get; set; }
        public Courses Courses { get; set; }
    }
}
