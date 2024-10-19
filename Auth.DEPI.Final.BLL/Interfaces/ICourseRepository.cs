using Auth.DEPI.Final.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DEPI.Final.BLL.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Courses>
    {
      Task<IEnumerable<Courses>>GetInstructorCoursesAsync(string Id);

    }
}
