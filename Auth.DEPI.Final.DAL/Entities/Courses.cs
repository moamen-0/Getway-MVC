using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.DEPI.Final.DAL.Entities
{
    public class Courses
    {
        public string? Id { get; set; }= Guid.NewGuid().ToString(); 
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    
        public string? Thumbnail { get; set; }

        public DateTime Created { get; set; }=DateTime.Now;

        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public ICollection<StudentCourses> StudentCourses { get; set; }
		public ICollection<Video> Videos { get; set; }
	}
}
