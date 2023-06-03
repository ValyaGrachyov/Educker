using Contract.ClientDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IInstructorService
    {
        public Task<IEnumerable<InstructorDto>> GetAllAsync();
        public Task<InstructorDto> FindById(int id);
        public Task<List<CourseDto>> GetInsturctorCourses(int id);
    }
}