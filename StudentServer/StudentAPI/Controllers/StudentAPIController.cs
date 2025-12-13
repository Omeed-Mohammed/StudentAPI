using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DataSimulation;
using StudentAPI.Model;
using System.Collections.Generic;

namespace StudentAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("All" , Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if(StudentDataSimulation.StudentsList.Count == 0)
                return NotFound("No Students Found!");

            return Ok(StudentDataSimulation.StudentsList); // Returns the List of Students.
        }



        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentDataSimulation.StudentsList.Where(student => student.Grade >= 50).ToList();

            if (passedStudents.Count == 0)
                return NotFound("No Students Passed!");

            return Ok(passedStudents);
        }



        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverageGrade()
        {
            if (StudentDataSimulation.StudentsList.Count == 0)
                return NotFound("No Students Found");

            var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            return Ok(averageGrade);
        }




        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> GetStudentById(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            return Ok(student);
        }
    }
}
