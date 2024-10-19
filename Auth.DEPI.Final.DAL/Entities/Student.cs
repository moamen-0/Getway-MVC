using Auth.DEPI.Final.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Auth.DEPI.Final.DAL.Entities
{
    public class Student : User
    {

        public ICollection<StudentCourses> StudentCourses { get; set; }
    }
}
