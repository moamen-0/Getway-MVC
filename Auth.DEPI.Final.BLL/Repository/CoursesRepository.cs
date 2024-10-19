using Auth.DEPI.Final.BLL.Interfaces;
using Auth.DEPI.Final.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Auth.DEPI.Final.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DEPI.Final.BLL.Repository
{
    public class CoursesRepository : GenericRepository<Courses>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CoursesRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Courses>> GetInstructorCoursesAsync(string Id)
        {

          
            return await _context.Courses.Where(c=>c.InstructorId == Id).ToListAsync();
        }
    }
}
