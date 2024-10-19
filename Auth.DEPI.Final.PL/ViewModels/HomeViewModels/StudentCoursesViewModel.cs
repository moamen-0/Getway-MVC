using Auth.DEPI.Final.DAL.Entities;

namespace Auth.DEPI.Final.PL.ViewModels.HomeViewModels
{
    public class StudentCoursesViewModel
    {
        public string StudentId { get; set; }
        public Student Student { get; set; }

        public string CourseId { get; set; }
        public Courses Course { get; set; }
    }
}
