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


        //for Add new we use Http Post

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> AddStudent(Student newStudent)
        {
            //Validate the Data
            if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age < 0 || newStudent.Grade < 0)
            {
                return BadRequest($"Invalid student data.");
            }

            newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1; 
            StudentDataSimulation.StudentsList.Add(newStudent);

            return CreatedAtRoute("GetStudentById" , new {id = newStudent.Id} , newStudent);
        }



        [HttpDelete("{id}" , Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeleteStudent(int id)
        {
            if(id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found .");
            }

            StudentDataSimulation.StudentsList.Remove(student);
            return Ok($"Student with ID {id} has been deleted.");
        }




        [HttpPut("{StudentID}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> UpdateStudent(Student updateStudent)
        {
            if(updateStudent == null || updateStudent.Id < 1 || string.IsNullOrEmpty(updateStudent.Name) 
                || updateStudent.Age < 0)
            {
                return BadRequest($"Invalid Student data .");
            }

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == updateStudent.Id);

            if(student == null)
            {
                return NotFound($"Student with ID {updateStudent.Id} not found .");
            }

            student.Name = updateStudent.Name;
            student.Age = updateStudent.Age;
            student.Grade = updateStudent.Grade;

            return Ok(student);

        }

    }
}
