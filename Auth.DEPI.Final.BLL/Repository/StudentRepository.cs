using Auth.DEPI.Final.BLL.Interfaces;
using Auth.DEPI.Final.DAL.Data.Context;
using Auth.DEPI.Final.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DEPI.Final.BLL.Repository
{
    public class StudentRepository : GenericRepository<Student> , IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) 
        {
            
        }
    }
}
