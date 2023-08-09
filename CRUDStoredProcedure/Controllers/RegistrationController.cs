using CRUDStoredProcedure.Interface;
using CRUDStoredProcedure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDStoredProcedure.Controllers
{
    [Authorize]
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IStudent _IStudent;

        public RegistrationController(IStudent iStudent)
        {
            _IStudent = iStudent;
        }

        [HttpGet]
        public async Task<List<Student>> Get()
        {
            return await Task.FromResult(_IStudent.GetStudentDetails());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Student student = _IStudent.GetStudentData(id);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound();
        }

        [HttpPost]
        public void Post(Student student)
        {
            _IStudent.AddStudent(student);
        }

        [HttpPut]
        public void Put(Student student)
        {
            _IStudent.UpdateStudentDetails(student);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _IStudent.DeleteStudent(id);
            return Ok();
        }
    }
}
